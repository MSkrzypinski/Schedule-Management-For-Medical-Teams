using Application.Mapper.Dtos;
using Application.MedicalTeams.CreateNewMedicalTeam;
using Application.MedicalTeams.UpdateMedicalTeam;
using Application.User.GetUser.GetUserByEmail;
using Domain.ValueObjects.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.MedicalTeams
{
    [Route("[controller]")]
    [ApiController]
    public class MedicalTeamController : Controller
    {
        private readonly IMediator _mediator;

        public MedicalTeamController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [Authorize(Roles = "Coordinator")]
        [HttpPost("create")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Guid>> CreateNewMedicalTeam([FromBody] CreateNewMedicalTeamCommand request)
        {
            var response = await _mediator.Send(request);

            return Ok(response);
        }
        //[Authorize(Roles = "Coordinator")]
        [HttpPut("update")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Guid>> UpdateMedicalTeam([FromBody] UpdateMedicalTeamCommand request)
        {
            var response = await _mediator.Send(request);

            return Ok(response);
        }
        
    }
}
