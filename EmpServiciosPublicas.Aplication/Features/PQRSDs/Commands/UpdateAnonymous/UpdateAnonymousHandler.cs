using AutoMapper;
using EmpServiciosPublicos.Domain;
using EmpServiciosPublicas.Aplication.Contracts.Insfrastructure;
using EmpServiciosPublicas.Aplication.Contracts.Persistence;
using EmpServiciosPublicas.Aplication.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace EmpServiciosPublicas.Aplication.Features.PQRSDs.Commands.UpdateAnonymous
{
    public class UpdateAnonymousHandler : IRequestHandler<UpdateAnonymousCommand>
    {
        private readonly IPQRSDRepository _ipqrsdRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateAnonymousHandler> _logger;
        private readonly IEmailService _emailService;

        public UpdateAnonymousHandler(IPQRSDRepository ipqrsdRepository, IMapper mapper, ILogger<UpdateAnonymousHandler> logger, IEmailService emailService)
        {
            _ipqrsdRepository = ipqrsdRepository;
            _mapper = mapper;
            _logger = logger;
            _emailService = emailService;
        }

        public async Task<Unit> Handle(UpdateAnonymousCommand request, CancellationToken cancellationToken)
        {
            var pqrsdUpdate = await _ipqrsdRepository.GetByIdAsync(request.Id);

            if (pqrsdUpdate == null)
                throw new NotFoundException(nameof(PQRSD), request.Id);

            //Otra forma de mappear
            _mapper.Map(request, pqrsdUpdate, typeof(UpdateAnonymousCommand), typeof(PQRSD));

            await _ipqrsdRepository.UpdateAsync(pqrsdUpdate);

            return Unit.Value;
        }
    }
}
