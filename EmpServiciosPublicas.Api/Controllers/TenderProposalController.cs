using EmpServiciosPublicas.Aplication.Features.TenderProposals.Commands.Create;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace EmpServiciosPublicas.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class TenderProposalController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TenderProposalController(IMediator mediator)
        {
            _mediator = mediator;
        }

        //[Authorize(Roles = "Administrator")]
        [HttpPost("Create")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<string>> Create([FromForm] CreateTenderProposalCommand command)
        {
            return await _mediator.Send(command);
        }
    }
}
