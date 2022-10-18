using EmpServiciosPublicas.Aplication.Features.Bidding.Commands.Create;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace EmpServiciosPublicas.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ProjectCallController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProjectCallController(IMediator mediator)
        {
            _mediator = mediator;
        }

        //[Authorize(Roles = "Administrator")]
        [HttpPost("Create")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<string>> CreateBidding([FromForm] CreateBiddingCommand command)
        {
            return await _mediator.Send(command);
        }
    }
}
