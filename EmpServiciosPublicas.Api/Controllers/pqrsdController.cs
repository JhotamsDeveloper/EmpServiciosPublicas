using EmpServiciosPublicas.Aplication.Features.PQRSDs.Queries.GetByCategory;
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

        [HttpGet("{category}", Name = "getPqrsdByCategory")]
        [ProducesResponseType(typeof(IEnumerable<PqrsdMv>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<PqrsdMv>>> GetPqrsdByCategory(string category)
        {
            var query = new GetPqrsdByCategoryQuery(category);
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}
