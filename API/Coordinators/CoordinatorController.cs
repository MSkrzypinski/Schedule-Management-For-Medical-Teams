using Application.Coordinators.CreateNewCoordinator;
using Application.Coordinators.GetCoordinator.GetCoordinatorById;
using Application.Mapper.Dtos;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Coordinators
{
    [Route("user/[controller]")]
    [ApiController]
    public class CoordinatorController : Controller
    {
        private readonly IMediator _mediator;

        public CoordinatorController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<CreateNewCoordinatorCommandResponse>> CreateNewCoordinator([FromBody] CreateNewCoordinatorCommand createNewCoordinatorCommand)
        {
            var response = await _mediator.Send(createNewCoordinatorCommand);
            return Ok(response);
        }
        [HttpGet("{id}", Name = "GetCoordinatorById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<CoordinatorDto>> GetCoordinatorById(Guid id)
        {
            var coordinatorDto = await _mediator.Send(new GetCoordinatorByIdQuery() { Id = id });
            return Ok(coordinatorDto);
        }
    }
}
