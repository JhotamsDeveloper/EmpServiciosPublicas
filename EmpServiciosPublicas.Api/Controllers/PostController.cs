using EmpServiciosPublicas.Aplication.Features.Posts.Commands.Create;
using EmpServiciosPublicas.Aplication.Features.Posts.Commands.Delete;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace EmpServiciosPublicas.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class PostController : Controller
    {
        private readonly IMediator _mediator;

        public PostController(IMediator mediator)
        {
            _mediator = mediator;
        }

        //[HttpPost(Name = "CreateAnonymousPQRSD")]
        //[Authorize(Roles = "Administrator")]
        [HttpPost("Create")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<string>> Create([FromForm] CreatePostCommand command)
        {
            return await _mediator.Send(command);
        }

        //[Authorize(Roles = "Administrator")]
        [HttpPut("Update")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Update([FromForm] UpdatePostCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
