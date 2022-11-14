using EmpServiciosPublicas.Aplication.Features.Posts.Commands.Create;
using EmpServiciosPublicas.Aplication.Features.Posts.Commands.Update;
using EmpServiciosPublicas.Aplication.Features.Posts.Models;
using EmpServiciosPublicas.Aplication.Features.Posts.Queries.GetAllPosts;
using EmpServiciosPublicas.Aplication.Features.Posts.Queries.PaginationPost;
using EmpServiciosPublicas.Aplication.Features.Shared.Queries;
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

        [HttpGet("GetAll")]
        [ProducesResponseType(typeof(IEnumerable<GetAllPostsMV>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<GetAllPostsMV>>> GetAllPQRSD()
        {
            var result = await _mediator.Send(new GetAllPostsQueries());
            return Ok(result);
        }

        [HttpGet("Pagination", Name = "Pagination of posts")]

        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<PaginationResponse<PostResponse>>> Pagination([FromQuery] PaginationPostQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}
