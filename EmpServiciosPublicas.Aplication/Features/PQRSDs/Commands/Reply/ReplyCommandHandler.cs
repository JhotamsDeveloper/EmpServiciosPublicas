using AutoMapper;
using EmpServiciosPublicas.Aplication.Constants;
using EmpServiciosPublicas.Aplication.Contracts.Insfrastructure;
using EmpServiciosPublicas.Aplication.Contracts.Persistence;
using EmpServiciosPublicas.Aplication.Exceptions;
using EmpServiciosPublicas.Aplication.Features.PQRSDs.Commands.Create;
using EmpServiciosPublicas.Aplication.Handlers;
using EmpServiciosPublicas.Aplication.Models;
using EmpServiciosPublicos.Domain;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace EmpServiciosPublicas.Aplication.Features.PQRSDs.Commands.Reply
{
    public class ReplyCommandHandler : IRequestHandler<ReplyCommand, string>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<CreateCommand> _logger;
        private readonly IEmailService _emailService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUploadFilesService _uploadFilesService;
        private readonly StorageSetting _storageSetting;
        private string? message = null;

        public ReplyCommandHandler(IMapper mapper, ILogger<CreateCommand> logger, IEmailService emailService, IUnitOfWork unitOfWork, IUploadFilesService uploadFilesService, IOptions<StorageSetting> storageSetting)
        {
            _mapper = mapper;
            _logger = logger;
            _emailService = emailService;
            _unitOfWork = unitOfWork;
            _uploadFilesService = uploadFilesService;
            _storageSetting = storageSetting.Value;
        }

        public async Task<string> Handle(ReplyCommand request, CancellationToken cancellationToken)
        {
            string nameFile;
            string path;
            string[] formatsArray;

            bool validateFiles;
            bool validateFileSize;

            int responseComplete;
            long size;

            PQRSD pqrsdUpdate;
            IReadOnlyList<PQRSD> pqrsdOld;

            formatsArray = _storageSetting.DocumentsFormats.Split(',');
            validateFiles = request.File!.ValidateCorrectFileFormat(formatsArray);
            if (!validateFiles)
            {
                _logger.LogError(message);
                throw new BadRequestException($"Los documentos debe de contener alguna de estas extensiones {string.Join(" ", formatsArray)}");
            }

            size = long.Parse(_storageSetting.Size);
            validateFileSize = request.File!.ValidateFileSize(size);
            if (!validateFileSize)
            {
                _logger.LogError(message);
                throw new BadRequestException($"El tamaño de los documentos debe de contener máximo {size / 1048576} mb");
            }

            pqrsdOld = await _unitOfWork.Repository<PQRSD>().GetAsync(x => x.Id == request.IdPqrsd, t => t.OrderByDescending(s => s.Id), "Storages", true);
            pqrsdUpdate = pqrsdOld.FirstOrDefault()!;

            if (pqrsdUpdate == null)
                throw new NotFoundException(nameof(PQRSD), request.IdPqrsd);

            _mapper.Map(request, pqrsdUpdate, typeof(ReplyCommand), typeof(PQRSD));
            pqrsdUpdate.PQRSDStatus = "Reply";
            await _unitOfWork.Repository<PQRSD>().UpdateAsync(pqrsdUpdate);
            responseComplete = await _unitOfWork.Complete();

            (nameFile, path) = await _uploadFilesService.UploadedFileAsync(request.File!, ProcessType.PQRSD.ToString(), Folder.Documents.ToString());
            Storage storage = new()
            {
                PqrsdId = request.IdPqrsd,
                NameFile = nameFile,
                RouteFile = path,
                Rol = Folder.Documents.ToString(),
                Availability = true
            };
            await _unitOfWork.Repository<Storage>().AddAsync(storage);
            int idStorege = await _unitOfWork.Complete();

            if (idStorege <= 0)
            {
                message = "No fue posible crear la solicitud";
                _logger.LogError(message);
                throw new BadRequestException(message);
            }

            pqrsdOld = await _unitOfWork.Repository<PQRSD>().GetAsync(x => x.Id == request.IdPqrsd, t => t.OrderByDescending(s => s.Id), "Storages", true);
            pqrsdUpdate = pqrsdOld.FirstOrDefault()!;

            return SerealizeExtension<PQRSD>
                .Serealize(pqrsdUpdate);
        }
    }
}
