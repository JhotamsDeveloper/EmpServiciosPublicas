using EmpServiciosPublicas.Aplication.Features.TenderProposals.Commands.Create;
using EmpServiciosPublicas.Aplication.Features.TenderProposals.Commands.Delete;
using EmpServiciosPublicas.Aplication.Features.TenderProposals.Commands.Update;
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

        //[Authorize(Roles = "Administrator")]
        [HttpPut("Update")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Update([FromForm] UpdateTenderProposalCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }

        //[Authorize(Roles = "Administrator")]
        [HttpDelete("Delete/{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Delete(Guid id)
        {
            await _mediator.Send(new DeleteTenderProposalCommand() { Id = id });
            return NoContent();
        }
    }
}
