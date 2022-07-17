using AutoMapper;
using EmpServiciosPublicos.Domain;
using EmpServiciosPublicas.Aplication.Contracts.Insfrastructure;
using EmpServiciosPublicas.Aplication.Contracts.Persistence;
using MediatR;
using Microsoft.Extensions.Logging;

namespace EmpServiciosPublicas.Aplication.Features.PQRSDs.Commands.CreateAnonymous
{
    public class CreateAnonymousCommandHandler : IRequestHandler<CreateAnonymousCommand, string>
    {
        //private readonly IPQRSDRepository _ipqrsdRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateAnonymousCommandHandler> _logger;
        private readonly IEmailService _emailService;
        private readonly IUnitOfWork _unitOfWork;

        public CreateAnonymousCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<CreateAnonymousCommandHandler> logger, IEmailService emailService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
            _emailService = emailService;
        }

        public async Task<string> Handle(CreateAnonymousCommand request, CancellationToken cancellationToken)
        {
            long tickes = DateTime.Now.Ticks;
            PQRSD pqrsdEntity = _mapper.Map<PQRSD>(request);

            pqrsdEntity.Ref = $"{pqrsdEntity.PQRSDType}-{ tickes:D20}";
            pqrsdEntity.Url = string.Join("-", pqrsdEntity.Title.Split('@', ',', '.', ';', '\'', ' ')).ToLower();
            pqrsdEntity.PQRSDStatus = "Create";

            _unitOfWork.PQRSDRepository.AddEntity(pqrsdEntity);
            int result = await _unitOfWork.Complete();

            if (result <= 0)
            {
                string message = "No se pudo insertar el PQRSD correctamente";
                _logger.LogError(message);
                throw new Exception(message);
            }

            //Envios de correo electrónico
            //....

            return pqrsdEntity.Ref;
        }
    }
}
