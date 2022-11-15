using EmpServiciosPublicas.Aplication.Features.Posts.Commands.Create;
using EmpServiciosPublicas.Aplication.Features.Posts.Commands.Delete;
using EmpServiciosPublicas.Aplication.Features.Posts.Commands.Update;
using EmpServiciosPublicas.Aplication.Features.Posts.Models;
using EmpServiciosPublicas.Aplication.Features.Posts.Queries.PaginationGetAllActivePosts;
using EmpServiciosPublicas.Aplication.Features.Posts.Queries.PaginationGetAllInactivePost;
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
        public async Task<ActionResult<PostResponse>> Create([FromForm] CreatePostCommand command)
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

        //[Authorize(Roles = "Administrator")]
        [HttpDelete("Delete/{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Delete(Guid id)
        {
            await _mediator.Send(new DeletePostCommand() { Id = id });
            return NoContent();
        }

        [HttpGet("GetAllActivePosts", Name = "Get all active posts")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<PaginationResponse<PostResponse>>> GetAllActivePosts([FromQuery] PaginationGetAllActivePostsQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("GetAllInactivePosts", Name = "Get all inactive posts")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<PaginationResponse<PostResponse>>> GetAllInactivePosts([FromQuery] PaginationGetAllInactivePostQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}
