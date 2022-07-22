using API.Shifts.Requests;
using Application.Shifts.AddDriver;
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

        [Authorize(Roles = "Coordinator")]
        [HttpPost("AddMedicalWorker")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Unit>> AddMedicalWorker([FromBody] AddMedicalWorkerRequest request,[FromQuery] MedicRole medicRole)
        {
            var response = await _mediator.Send(new AddMedicalWorkerCommand()
            {
                ShiftId= request.ShiftId,
                MedicalWorkerId=request.MedicalWorkerId,
                MedicRole = medicRole
            });

            return Ok(response);
        }
      
    }
}
