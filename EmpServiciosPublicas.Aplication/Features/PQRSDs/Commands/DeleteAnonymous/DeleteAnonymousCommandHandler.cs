using AutoMapper;
using EmpServiciosPublicas.Aplication.Constants;
using EmpServiciosPublicas.Aplication.Contracts.Insfrastructure;
using EmpServiciosPublicas.Aplication.Contracts.Persistence;
using EmpServiciosPublicas.Aplication.Exceptions;
using EmpServiciosPublicos.Domain;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace EmpServiciosPublicas.Aplication.Features.PQRSDs.Commands.DeleteAnonymous
{
    public class DeleteAnonymousCommandHandler : IRequestHandler<DeleteAnonymousCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<DeleteAnonymousCommand> _logger;
        private readonly IEmailService _emailService;
        private readonly IUploadFilesService _uploadFilesService;
        private readonly IConfiguration _configuration;

        public DeleteAnonymousCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<DeleteAnonymousCommand> logger, IEmailService emailService, IUploadFilesService uploadFilesService, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
            _emailService = emailService;
            _uploadFilesService = uploadFilesService;
            _configuration = configuration;
        }

        public async Task<Unit> Handle(DeleteAnonymousCommand request, CancellationToken cancellationToken)
        {
            int responseComplete;

            PQRSD pqrsdDelete;

            IReadOnlyList<PQRSD> pqrsdOld;

            string message;

            pqrsdOld = await _unitOfWork.Repository<PQRSD>().GetAsync(x => x.Id == request.Id, t => t.OrderByDescending(s => s.Id), "Storages", true);
            pqrsdDelete = pqrsdOld.FirstOrDefault();

            if (pqrsdDelete == null)
                throw new NotFoundException(nameof(PQRSD), request.Id);

            if (pqrsdDelete.Storages.Any())
            {
                foreach (var file in pqrsdDelete.Storages)
                {
                    await _unitOfWork.Repository<Storage>().DeleteAsync(file);
                }
            }

            await _unitOfWork.Repository<PQRSD>().DeleteAsync(pqrsdDelete);
            responseComplete = await _unitOfWork.Complete();

            if (pqrsdDelete.Storages.Any())
            {
                foreach (var file in pqrsdDelete.Storages)
                {
                    await _uploadFilesService.DeleteUploadAsync(file.NameFile, ProcessType.PQRSD.ToString(), Folder.Documents.ToString());
                }
            }

            message = $"El pqrsd con id {request.Id} fue elimado exitosamente";
            _logger.LogInformation(message);

            return Unit.Value;
        }
    }
}
