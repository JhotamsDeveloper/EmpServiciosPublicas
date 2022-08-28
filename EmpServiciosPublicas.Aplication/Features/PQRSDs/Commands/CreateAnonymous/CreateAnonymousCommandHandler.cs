using AutoMapper;
using EmpServiciosPublicos.Domain;
using EmpServiciosPublicas.Aplication.Contracts.Insfrastructure;
using EmpServiciosPublicas.Aplication.Contracts.Persistence;
using MediatR;
using Microsoft.Extensions.Logging;
using EmpServiciosPublicas.Aplication.Constants;

namespace EmpServiciosPublicas.Aplication.Features.PQRSDs.Commands.CreateAnonymous
{
    public class CreateAnonymousCommandHandler : IRequestHandler<CreateAnonymousCommand, string>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<CreateAnonymousCommandHandler> _logger;
        private readonly IEmailService _emailService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUploadFilesService _uploadFilesService;

        public CreateAnonymousCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<CreateAnonymousCommandHandler> 
            logger, IEmailService emailService, IUploadFilesService uploadFilesService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
            _emailService = emailService;
            _uploadFilesService = uploadFilesService;
        }

        public async Task<string> Handle(CreateAnonymousCommand request, CancellationToken cancellationToken)
        {
            long tickes = DateTime.Now.Ticks;
            PQRSD pqrsdEntity = _mapper.Map<PQRSD>(request);

            pqrsdEntity.Ref = $"{pqrsdEntity.PQRSDType}-{ tickes:D20}";
            pqrsdEntity.Url = string.Join("-", pqrsdEntity.Title!.Split('@', ',', '.', ';', '\'', ' ')).ToLower();
            pqrsdEntity.PQRSDStatus = "Create";

            _unitOfWork.PQRSDRepository.AddEntity(pqrsdEntity);
            int idpqrsd = await _unitOfWork.Complete();

            string message;
            if (idpqrsd <= 0)
            {
                message = "No se pudo insertar el PQRSD correctamente";
                _logger.LogError(message);
                throw new Exception(message);
            }
            else if (request.Files == null)
            {
                message = "Es necesario adjuntar un documento relacionado a su petición";
                _logger.LogError(message);
                throw new Exception(message);
            }
            else if (!request.Files.Any())
            {
                message = "Es necesario adjuntar un documento relacionado a su petición";
                _logger.LogError(message);
                throw new Exception(message);
            }

            foreach (var file in request.Files)
            {
                var (nameFile, path) = await _uploadFilesService.UploadedFileAsync(file, ProcessType.PQRSD.ToString(), Folder.Documents.ToString());
                Storage storage = new()
                {
                    PqrsdId = idpqrsd,
                    NameFile = nameFile,
                    RouteFile = path,
                    Rol = Folder.Documents.ToString(),
                    Availability = true
                };
                await _unitOfWork.Repository<Storage>().AddAsync(storage);
            }

            //Envios de correo electrónico
            //....

            return pqrsdEntity.Ref;
        }
    }
}
