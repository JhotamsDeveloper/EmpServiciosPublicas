using AutoMapper;
using EmpServiciosPublicas.Aplication.Constants;
using EmpServiciosPublicas.Aplication.Contracts.Insfrastructure;
using EmpServiciosPublicas.Aplication.Contracts.Persistence;
using EmpServiciosPublicas.Aplication.Exceptions;
using EmpServiciosPublicas.Aplication.Handlers;
using EmpServiciosPublicas.Aplication.Models;
using EmpServiciosPublicos.Domain;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace EmpServiciosPublicas.Aplication.Features.PQRSDs.Commands.UpdateAnonymous
{
    public class UpdateAnonymousCommandHandler : IRequestHandler<UpdateAnonymousCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateAnonymousCommandHandler> _logger;
        private readonly IUploadFilesService _uploadFilesService;
        private readonly StorageSetting _storageSetting;

        public UpdateAnonymousCommandHandler(IMapper mapper, ILogger<UpdateAnonymousCommandHandler> logger, IUnitOfWork unitOfWork, IUploadFilesService uploadFilesService, IOptions<StorageSetting> storageSetting)
        {
            _mapper = mapper;
            _logger = logger;
            _unitOfWork = unitOfWork;
            _uploadFilesService = uploadFilesService;
            _storageSetting = storageSetting.Value;
        }

        public async Task<Unit> Handle(UpdateAnonymousCommand request, CancellationToken cancellationToken)
        {
            string nameFile;
            string path;
            string[] formatsArray;
            string message = string.Empty;

            bool validateFiles;
            bool validateFileSize;

            int responseComplete;
            long size;
            long tickes = DateTime.Now.Ticks;

            PQRSD pqrsdUpdate;
            IReadOnlyList<PQRSD> pqrsdOld;

            formatsArray = _storageSetting.DocumentsFormats.Split(',');
            validateFiles = request.Files!.ValidateCorrectFileFormat(formatsArray);
            if (!validateFiles)
            {
                _logger.LogError(message);
                throw new BadRequestException($"Los documentos debe de contener alguna de estas extensiones {string.Join(" ", formatsArray)}");
            }

            size = long.Parse(_storageSetting.Size);
            validateFileSize = request.Files!.ValidateFileSize(size);
            if (!validateFileSize)
            {
                _logger.LogError(message);
                throw new BadRequestException($"El tamaño de los documentos debe de contener máximo {size / 1048576} mb");
            }

            pqrsdOld = await _unitOfWork.Repository<PQRSD>().GetAsync(x => x.Id == request.Id, t => t.OrderByDescending(s => s.Id), "Storages", true);
            pqrsdUpdate = pqrsdOld.FirstOrDefault();

            if (pqrsdUpdate == null)
                throw new NotFoundException(nameof(PQRSD), request.Id);


            if (pqrsdUpdate.Storages.Any())
            {
                foreach (var file in pqrsdUpdate.Storages)
                {
                    await _uploadFilesService.DeleteUploadAsync(file.NameFile, ProcessType.PQRSD.ToString(), Folder.Documents.ToString());
                    await _unitOfWork.Repository<Storage>().DeleteAsync(file);
                    await _unitOfWork.Complete();
                }
            }

            _mapper.Map(request, pqrsdUpdate, typeof(UpdateAnonymousCommand), typeof(PQRSD));
            pqrsdUpdate.PQRSDStatus = "Update";
            await _unitOfWork.Repository<PQRSD>().UpdateAsync(pqrsdUpdate);
            responseComplete = await _unitOfWork.Complete();

            if (responseComplete <= 0)
            {
                message = "No fue posible crear un PQRSD correctamente";
                _logger.LogError(message);
                throw new BadRequestException(message);
            }

            foreach (IFormFile file in request.Files)
            {
                (nameFile, path) = await _uploadFilesService.UploadedFileAsync(file, ProcessType.PQRSD.ToString(), Folder.Documents.ToString());
                Storage storage = new()
                {
                    PqrsdId = request.Id,
                    NameFile = nameFile,
                    RouteFile = path,
                    Rol = Folder.Documents.ToString(),
                    Availability = true
                };
                await _unitOfWork.Repository<Storage>().AddAsync(storage);
                var idStorege = await _unitOfWork.Complete();
            }

            return Unit.Value;
        }
    }
}
