using EmpServiciosPublicas.Aplication.Features.PQRSDs.Commands.CreateAnonymous;
using EmpServiciosPublicas.Aplication.Features.PQRSDs.Commands.DeleteAnonymous;
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
    public class PqrsdController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PqrsdController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{Id}", Name = "GetByType")]
        //[Authorize(Roles = "Administrator")]
        [ProducesResponseType(typeof(IEnumerable<PqrsdMv>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<PqrsdMv>>> GetPqrsdByTypePqrsd(string Id)
        {
            var result = await _mediator.Send(new GetPqrsdByTypePqrsdQuery(Id));
            return Ok(result);
        }

        [HttpPost(Name = "CreateAnonymousPQRSD")]
        //[Authorize(Roles = "Administrator")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<string>> CreateAnonymousPQRSD([FromForm] CreateAnonymousCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpPut(Name = "UpdateAnonymousPQRSD")]
        //[Authorize(Roles = "Administrator")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> UpdateAnonymousPQRSD([FromForm] UpdateAnonymousCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{id}", Name = "DeleteAnonymousPQRSD")]
        //[Authorize(Roles = "Administrator")]
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
