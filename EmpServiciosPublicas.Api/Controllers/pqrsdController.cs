using EmpServiciosPublicas.Aplication.Features.PQRSDs.Queries.GetPqrsdByTypePqrsd;
using MediatR;
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

        [HttpGet("{typePqrsd}", Name = "GetPqrsdByTypePqrsd")]
        [ProducesResponseType(typeof(IEnumerable<PqrsdMv>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<PqrsdMv>>> GetPqrsdByTypePqrsd(string typePqrsd)
        {
            var query = new GetPqrsdByTypePqrsdQuery(typePqrsd);
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}
