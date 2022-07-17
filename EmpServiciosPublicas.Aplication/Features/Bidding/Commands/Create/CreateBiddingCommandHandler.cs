using AutoMapper;
using EmpServiciosPublicas.Aplication.Contracts.Persistence;
using MediatR;
using Microsoft.Extensions.Logging;
using modelEntity = EmpServiciosPublicos.Domain;

namespace EmpServiciosPublicas.Aplication.Features.Bidding.Commands.Create
{
    public class CreateBiddingCommandHandler : IRequestHandler<CreateBiddingCommand, int>
    {
        private readonly ILogger<CreateBiddingCommandHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CreateBiddingCommandHandler(ILogger<CreateBiddingCommandHandler> logger, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(CreateBiddingCommand request, CancellationToken cancellationToken)
        {
            modelEntity.Bidding biddingEntity = _mapper.Map<modelEntity.Bidding>(request);

            biddingEntity.Url = string.Join("-", biddingEntity.Title.Split('@', ',', '.', ';', '\'',' ')).ToLower();

            _unitOfWork.Repository<modelEntity.Bidding>().AddEntity(biddingEntity);
            var result = await _unitOfWork.Complete();

            if (result <= 0)
            {
                string message = "No se pudo insertar la licitación correctamente";
                _logger.LogError(message);
                throw new Exception(message);
            }

            return result;

        }
    }
}
