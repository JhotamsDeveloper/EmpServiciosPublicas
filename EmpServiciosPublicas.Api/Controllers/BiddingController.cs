using EmpServiciosPublicas.Aplication.Features.Bidding.Commands.Create;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace EmpServiciosPublicas.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class BiddingController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BiddingController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost(Name = "CreateBidding")]
        //[Authorize(Roles = "Administrator")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<int>> CreateBidding([FromBody] CreateBiddingCommand command)
        {
            return await _mediator.Send(command);
        }
    }
}
