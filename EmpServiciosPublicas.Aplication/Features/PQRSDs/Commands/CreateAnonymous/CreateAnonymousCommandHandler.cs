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
        private readonly IPQRSDRepository _ipqrsdRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateAnonymousCommandHandler> _logger;
        private readonly IEmailService _emailService;

        public CreateAnonymousCommandHandler(IPQRSDRepository ipqrsdRepository, IMapper mapper, ILogger<CreateAnonymousCommandHandler> logger, IEmailService emailService)
        {
            _ipqrsdRepository = ipqrsdRepository;
            _mapper = mapper;
            _logger = logger;
            _emailService = emailService;
        }

        public async Task<string> Handle(CreateAnonymousCommand request, CancellationToken cancellationToken)
        {
            long tickes = DateTime.Now.Ticks;
            PQRSD pqrsdEntity = _mapper.Map<PQRSD>(request);

            pqrsdEntity.Ref = $"{pqrsdEntity.PQRSDType}-{ tickes:D20}";
            pqrsdEntity.Url = $"{pqrsdEntity.Title.Replace(" ", "")}";
            pqrsdEntity.PQRSDStatus = "Create";

            PQRSD newPqrsd = await _ipqrsdRepository.AddAsync(pqrsdEntity);
            string message = $"The PQRSD has been created successfully; ticker {newPqrsd.Ref}";
            _logger.LogInformation(message);

            //Envios de correo electrónico
            return newPqrsd.Ref;
        }
    }
}
