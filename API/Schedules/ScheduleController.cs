using API.MedicalWorkers.Requests;
using Application.Schedules.CreateNewSchedule;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Schedules
{
    [Route("user/[controller]")]
    [ApiController]
    public class ScheduleController : Controller
    {
        private readonly IMediator _mediator;

        public ScheduleController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("AddNewSchedule")]
        [Authorize(Roles = "Coordinator")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Guid>> CreateNewSchedule([FromBody] CreateNewScheduleCommand request)
        {
            var response = await _mediator.Send(request);

            return Ok(response);
        }
       
    }
}
