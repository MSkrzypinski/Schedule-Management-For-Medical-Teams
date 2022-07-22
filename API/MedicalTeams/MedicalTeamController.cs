using Application.MedicalTeams.CreateNewMedicalTeam;
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
    [Route("user/[controller]")]
    [ApiController]
    public class MedcialTeamController : Controller
    {
        private readonly IMediator _mediator;

        public MedcialTeamController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [Authorize(Roles = "Coordinator")]
        [HttpPost("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Guid>> CreateNewMedicalTeam([FromBody] CreateNewMedicalTeamRequest request,[FromQuery] MedicalTeamType medicalTeamType)
        {
            var response = await _mediator.Send(new CreateNewMedicalTeamCommand()
            {
                CoordinatorId=request.CoordinatorId,
                City = request.City,
                Code = request.Code,
                SizeOfTeam = request.SizeOfTeam,
                MedicalTeamType = medicalTeamType
            });

            return Ok(response);
        }
    }
}
