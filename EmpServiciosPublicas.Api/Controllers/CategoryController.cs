﻿using EmpServiciosPublicas.Aplication.Features.Categories.Commands.Create;
using EmpServiciosPublicas.Aplication.Features.Categories.Commands.Delete;
using EmpServiciosPublicas.Aplication.Features.PQRSDs.Commands.DeleteAnonymous;
using EmpServiciosPublicas.Aplication.Features.PQRSDs.Commands.Update;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace EmpServiciosPublicas.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CategoryController : Controller
    {
        private readonly IMediator _mediator;

        public CategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        //[HttpPost(Name = "CreateAnonymousPQRSD")]
        //[Authorize(Roles = "Administrator")]
        [HttpPost("Create")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<string>> Create([FromForm] CreateCategoryCommand command)
        {
            return await _mediator.Send(command);
        }

        //[Authorize(Roles = "Administrator")]
        [HttpPut("Update")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Update([FromForm] UpdateCategoryCommand command)
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
            await _mediator.Send(new DeleteCategoryCommand() { Id = id });
            return NoContent();
        }
    }
}
