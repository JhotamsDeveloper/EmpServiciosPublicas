using EmpServiciosPublicas.Aplication.Features.PQRSDs.Commands.Create;
using EmpServiciosPublicas.Aplication.Features.PQRSDs.Commands.CreateAnonymous;
using EmpServiciosPublicas.Aplication.Features.PQRSDs.Commands.DeleteAnonymous;
using EmpServiciosPublicas.Aplication.Features.PQRSDs.Commands.Update;
using EmpServiciosPublicas.Aplication.Features.PQRSDs.Commands.UpdateAnonymous;
using EmpServiciosPublicas.Aplication.Features.PQRSDs.Queries.GetPqrsdByTypePqrsd;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace EmpServiciosPublicas.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class PQRSDController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PQRSDController(IMediator mediator)
        {
            _mediator = mediator;
        }

        //[Authorize(Roles = "Administrator")]
        [HttpGet("GetPqrsdByType/{id?}")]
        [ProducesResponseType(typeof(IEnumerable<PqrsdMv>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<PqrsdMv>>> GetPqrsdByTypePqrsd(string Id)
        {
            var result = await _mediator.Send(new GetPqrsdByTypePqrsdQuery(Id));
            return Ok(result);
        }

        //[Authorize(Roles = "Administrator")]
        [HttpPost("CreateAnonymous")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<string>> CreateAnonymousPQRSD([FromForm] CreateAnonymousCommand command)
        {
            return await _mediator.Send(command);
        }

        //[Authorize(Roles = "Administrator")]
        [HttpPost("Create")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<string>> CreatePQRSD([FromForm] CreateCommand command)
        {
            return await _mediator.Send(command);
        }

        //[Authorize(Roles = "Administrator")]
        [HttpPut("UpdateAnonymous")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> UpdateAnonymousPQRSD([FromForm] UpdateAnonymousCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }

        //[Authorize(Roles = "Administrator")]
        [HttpPut("Update")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Update([FromForm] UpdateCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }

        //[Authorize(Roles = "Administrator")]
        [HttpDelete("Delete/{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> DeleteAnonymousPQRSD(int id)
        {
            await _mediator.Send(new DeleteAnonymousCommand() { Id = id});
            return NoContent();
        }
    }
}
