using AutoMapper;
using EmpServiciosPublicas.Aplication.Constants;
using EmpServiciosPublicas.Aplication.Contracts.Insfrastructure;
using EmpServiciosPublicas.Aplication.Contracts.Persistence;
using EmpServiciosPublicas.Aplication.Exceptions;
using EmpServiciosPublicas.Aplication.Models;
using EmpServiciosPublicos.Domain;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using BiddingEntity = EmpServiciosPublicos.Domain.Bidding;

namespace EmpServiciosPublicas.Aplication.Features.TenderProposals.Commands.Delete
{
    public class DeleteTenderProposalCommandHandle : IRequestHandler<DeleteTenderProposalCommand>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<DeleteTenderProposalCommandHandle> _logger;
        private readonly IEmailService _emailService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUploadFilesService _uploadFilesService;
        private readonly StorageSetting _storageSetting;
        private string? message = null;

        public DeleteTenderProposalCommandHandle(IMapper mapper,
                                                 ILogger<DeleteTenderProposalCommandHandle> logger,
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

        public async Task<Unit> Handle(DeleteTenderProposalCommand request, CancellationToken cancellationToken)
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

            tenderProposalOldList = await _unitOfWork.Repository<TenderProposal>().GetAsync(x => x.Id == request.Id, t => t.OrderByDescending(s => s.Id), "Storages", true);
            tenderProposalEntity = tenderProposalOldList.FirstOrDefault()!;

            if (tenderProposalEntity.Storages.Any())
            {
                foreach (var file in tenderProposalEntity.Storages)
                {
                    await _uploadFilesService.DeleteUploadAsync(file.NameFile, ProcessType.TenderProposal.ToString(), Folder.Documents.ToString());
                    await _unitOfWork.Repository<Storage>().DeleteAsync(file);
                    await _unitOfWork.Complete();
                }
            }
            await _unitOfWork.Repository<TenderProposal>().DeleteAsync(tenderProposalEntity);
            responseComplete = await _unitOfWork.Complete();

            if (tenderProposalEntity.Storages.Any())
            {
                foreach (var file in tenderProposalEntity.Storages)
                {
                    await _uploadFilesService.DeleteUploadAsync(file.NameFile, ProcessType.TenderProposal.ToString(), Folder.Documents.ToString());
                }
            }

            message = $"El solicitud de propuesta con id {request.Id} fue elimado exitosamente";
            _logger.LogInformation(message);

            return Unit.Value;
        }
    }
}
