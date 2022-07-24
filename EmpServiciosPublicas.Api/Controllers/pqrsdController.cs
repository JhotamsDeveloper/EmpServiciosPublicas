using EmpServiciosPublicas.Aplication.Features.PQRSDs.Commands.CreateAnonymous;
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


        [HttpPost(Name = "CreateAnonymousPQRSD")]
        //[Authorize(Roles = "Administrator")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<string>> CreateAnonymousPQRSD([FromForm] CreateAnonymousCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpGet("{typePqrsd}", Name = "GetByType")]
        [Authorize(Roles = "Administrator")]
        [ProducesResponseType(typeof(IEnumerable<PqrsdMv>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<PqrsdMv>>> GetPqrsdByTypePqrsd(string typePqrsd)
        {
            var query = new GetPqrsdByTypePqrsdQuery(typePqrsd);
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}
