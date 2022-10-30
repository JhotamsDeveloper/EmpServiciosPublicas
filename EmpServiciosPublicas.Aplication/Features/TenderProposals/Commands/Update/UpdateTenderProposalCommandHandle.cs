using AutoMapper;
using EmpServiciosPublicas.Aplication.Constants;
using EmpServiciosPublicas.Aplication.Contracts.Insfrastructure;
using EmpServiciosPublicas.Aplication.Contracts.Persistence;
using EmpServiciosPublicas.Aplication.Exceptions;
using EmpServiciosPublicas.Aplication.Features.PQRSDs.Commands.UpdateAnonymous;
using EmpServiciosPublicas.Aplication.Features.TenderProposals.Commands.Create;
using EmpServiciosPublicas.Aplication.Handlers;
using EmpServiciosPublicas.Aplication.Models;
using EmpServiciosPublicos.Domain;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using BiddingEntity = EmpServiciosPublicos.Domain.Bidding;

namespace EmpServiciosPublicas.Aplication.Features.TenderProposals.Commands.Update
{
    public class UpdateTenderProposalCommandHandle : IRequestHandler<UpdateTenderProposalCommand>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<CreateTenderProposalCommandHandle> _logger;
        private readonly IEmailService _emailService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUploadFilesService _uploadFilesService;
        private readonly StorageSetting _storageSetting;
        private string? message = null;

        public UpdateTenderProposalCommandHandle(IMapper mapper,
                                                 ILogger<CreateTenderProposalCommandHandle> logger,
                                                 IEmailService emailService,
                                                 IUnitOfWork unitOfWork,
                                                 IUploadFilesService uploadFilesService,
                                                 IOptions<StorageSetting> storageSetting)
        {
            _mapper = mapper;
            _logger = logger;
            _emailService = emailService;
            _unitOfWork = unitOfWork;
            _uploadFilesService = uploadFilesService;
            _storageSetting = storageSetting.Value;
        }

        public async Task<Unit> Handle(UpdateTenderProposalCommand request, CancellationToken cancellationToken)
        {
            string nameFile;
            string path;
            string[] formatsArray;

            bool validateFiles;
            bool validateFileSize;

            int responseComplete;
            int size;

            TenderProposal tenderProposalEntity;
            Storage storage;

            IReadOnlyList<TenderProposal> tenderProposalOldList;

            var bidding = await _unitOfWork.Repository<BiddingEntity>().GetByIdAsync(request.BiddingId);
            if (bidding == null)
            {
                _logger.LogError(message);
                throw new BadRequestException($"No fue posible actualizar la solicitud porque no se encontro la convocatoria");
            }

            tenderProposalOldList = await _unitOfWork.Repository<TenderProposal>().GetAsync(x => x.Id == request.Id, t => t.OrderByDescending(s => s.Id), "Storages", true);
            tenderProposalEntity = tenderProposalOldList.FirstOrDefault()!;

            if (tenderProposalEntity == null)
                throw new NotFoundException(nameof(PQRSD), request.Id);

            formatsArray = _storageSetting.DocumentsFormats.Split(',');
            validateFiles = request.File!.ValidateCorrectFileFormat(formatsArray);
            if (!validateFiles)
            {
                _logger.LogError(message);
                throw new BadRequestException($"Los documentos debe de contener alguna de estas extensiones {string.Join(" ", formatsArray)}");
            }

            size = int.Parse(_storageSetting.Size);
            validateFileSize = request.File!.ValidateFileSize(size);
            if (!validateFileSize)
            {
                _logger.LogError(message);
                throw new BadRequestException($"El tamaño de los documentos debe de contener máximo {size / 1048576} mb");
            }

            if (tenderProposalEntity.Storages.Any())
            {
                foreach (var file in tenderProposalEntity.Storages)
                {
                    await _uploadFilesService.DeleteUploadAsync(file.NameFile, ProcessType.TenderProposal.ToString(), Folder.Documents.ToString());
                    await _unitOfWork.Repository<Storage>().DeleteAsync(file);
                    await _unitOfWork.Complete();
                }
            }

            _mapper.Map(request, tenderProposalEntity, typeof(UpdateTenderProposalCommand), typeof(TenderProposal));
            await _unitOfWork.Repository<TenderProposal>().UpdateAsync(tenderProposalEntity);
            responseComplete = await _unitOfWork.Complete();
            if (responseComplete <= 0)
            {
                message = "No fue posible crear la solicitud correctamente";
                _logger.LogError(message);
                throw new BadRequestException(message);
            }

            (nameFile, path) = await _uploadFilesService.UploadedFileAsync(request.File, ProcessType.TenderProposal.ToString(), Folder.Documents.ToString());
            storage = new()
            {
                TenderProposalId = tenderProposalEntity.Id,
                NameFile = nameFile,
                RouteFile = path,
                Rol = Folder.Documents.ToString(),
                Availability = true
            };
            await _unitOfWork.Repository<Storage>().AddAsync(storage);
            await _unitOfWork.Complete();

            return Unit.Value;
        }
    }
}
