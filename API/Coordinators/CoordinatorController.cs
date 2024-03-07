using Application.Coordinators.CreateNewCoordinator;
using Application.Coordinators.GetAllMedicalTeamsAssignedToCoordinator;
using Application.Coordinators.GetCoordinator.GetCoordinatorById;
using Application.Mapper.Dtos;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Coordinators
{
    [Route("[controller]")]
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
        public async Task<ActionResult<Guid>> CreateNewCoordinator([FromBody] CreateNewCoordinatorCommand createNewCoordinatorCommand)
        {
            var response = await _mediator.Send(createNewCoordinatorCommand);
            return Ok(response);
        }
        
        //[Authorize(Roles = "Coordinator")]
        [HttpGet("user/{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<CoordinatorDto>> GetCoordinatorByUserId(Guid userId)
        {
            var coordinatorDto = await _mediator.Send(new GetCoordinatorByUserIdQuery() { UserId = userId });
            return Ok(coordinatorDto);
        }
        [HttpGet("get/medicalTeams")]
        //[Authorize(Roles = "Coordinator")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<MedicalTeamDto>>> GetMedicalTeamsByUserId(Guid userId)
        {
            var medicalTeamDtos = await _mediator.Send(new GetAllMedicalTeamsAssignedToCoordinatorQuery() { UserId = userId });
            return Ok(medicalTeamDtos);
        }

    }
}
