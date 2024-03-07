using Application.Mapper.Dtos;
using Application.Shifts.AddMedicalWorkerToShift;
using Application.Shifts.GetShiftsByMonthAndYearAndUserId;
using Application.Shifts.GetShiftsByScheduleId;
using Application.Shifts.PublishShift;
using Domain.Entities;
using Domain.ValueObjects.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Shifts
{
    [Route("user/[controller]")]
    [ApiController]
    public class ShiftController : Controller
    {
        private readonly IMediator _mediator;

        public ShiftController(IMediator mediator)
        {
            _mediator = mediator;
        }

        //[Authorize(Roles = "Coordinator")]
        [HttpPut("AddMedicalWorkerToShift")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Unit>> AddMedicalWorker([FromBody] AddMedicalWorkerCommand request)
        {
            var response = await _mediator.Send(request);

            return Ok(response);
        }
        
        [Authorize(Roles = "Coordinator")]
        [HttpPost("PublishShift")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Unit>> Publish(Guid shiftId)
        {
            var response = await _mediator.Send(new PublishShiftCommand()
            {
                ShiftId = shiftId
            });

            return Ok(response);
        }
        [HttpGet("getByScheduleId/{scheduleId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IList<Shift>>> GetShiftsByScheduleId(Guid scheduleId)
        {
            var response = await _mediator.Send(new GetShiftsByScheduleIdQuery() {ScheduleId = scheduleId});

            return Ok(response);
        }
         [HttpGet("GetShiftsByMonthAndYearAndUserId")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IList<Shift>>> GetShiftsByMonthAndYearAndUserId([FromQuery] GetShiftsByMonthAndYearAndUserIdQuery request)
        {
            var response = await _mediator.Send(request);

            return Ok(response);
        }
    }
}
