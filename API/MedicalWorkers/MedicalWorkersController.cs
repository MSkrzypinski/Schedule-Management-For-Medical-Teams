using Application;
using Application.Coordinators.GetAllMedicalTeamsAssignedToCoordinator;
using Application.Mapper.Dtos;
using Application.MedicalWorkers.AddDayOff;
using Application.MedicalWorkers.AddMedicalWorkerProfession;
using Application.MedicalWorkers.CreateNewEmploymentContract;
using Application.MedicalWorkers.CreateNewMedicalWorker;
using Application.MedicalWorkers.DeleteMedicalWorkerProfession;
using Application.MedicalWorkers.GetMedicalWorkerAssignedToCoordinator;
using Application.MedicalWorkers.UpdateMedicalWorker;
using Application.MedicalWorkers.GetMedicRoleToMedicalTeam;
using Domain.ValueObjects;
using Domain.ValueObjects.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.MedicalWorkers.DeleteMedicalWorkerEmploymentContract;
using Domain.Entities;
using Application.MedicalWorkers.GetMedicalWorkersAssignedToMedcialTeamByMedcialTeamId;
using Application.MedicalWorkers.GetMedicalWorkerByUserId;

namespace API.MedicalWorkers
{
    [Route("user/[controller]")]
    [ApiController]
    public class MedicalWorkersController : Controller
    {
        private readonly IMediator _mediator;

        public MedicalWorkersController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [Authorize(Roles = "Coordinator")]
        [HttpPost("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Guid>> CreateNewMedicalWorker([FromBody] CreateNewMedicalWorkerCommand request)
        {
            var response = await _mediator.Send(request);

            return Ok(response);
        }

        //[Authorize(Roles = "Coordinator")]
        [HttpPut("AddMedicalWorkerProfession")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Unit>> AddMedicalWorkerProfession([FromBody] AddMedicalWorkerProfessionCommand request)
        {
            var response = await _mediator.Send(request); 

            return Ok(response);
        }

        //[Authorize(Roles = "Coordinator")]
        [HttpPost("CreateNewEmploymentContract")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Unit>> CreateNewEmploymentContract([FromBody] CreateNewEmploymentContractCommand request)
        {
            var response = await _mediator.Send(request);

            return Ok(response);
        }
        //[Authorize(Roles = "MedicalWorker")]
        [HttpPut("AddDayOff")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Unit>> AddDayOff([FromBody] AddDayOffCommand addDayOffCommand)
        {
            var response = await _mediator.Send(addDayOffCommand);

            return Ok(response);
        }
        [HttpGet("get/{coordinatorId}")]
        //[Authorize(Roles = "Coordinator")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<MedicalWorkerDto>>> GetMedicalWorkersByUserId(Guid coordinatorId)
        {
            var medicalWorkersDtos = await _mediator.Send(new GetMedicalWorkersAssignedToCoordinatorQuery() { CoordinatorId = coordinatorId });
            return Ok(medicalWorkersDtos);
        }
         //[Authorize(Roles = "Coordinator")]
        [HttpPut("update")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Guid>> UpdateMedicalWorker([FromBody] UpdateMedicalWorkerCommand request)
        {
            var response = await _mediator.Send(request);

            return Ok(response);
        }
        //[Authorize(Roles = "Coordinator")]
        [HttpPut("delete/profession")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Guid>> UpdateMedicalWorker([FromBody] DeleteMedicalWorkerProfessionCommand request)
        {
            var response = await _mediator.Send(request);

            return Ok(response);
        }
        [HttpGet("professionToPermission")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Guid>> ProfessionToPermission([FromQuery] GetProfessionToPermissionCommand request)
        {
            var response = await _mediator.Send(request);

            return Ok(response);
        }
        [HttpGet("GetMedicRoleToMedicalTeam")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IList<MedicalWorkerProfessionsToPermissions>>> GetMedicRoleToMedicalTeam([FromQuery] GetMedicalWorkerProfessionToMedicalTeamQuery request)
        {
            var response = await _mediator.Send(request);

            return Ok(response);
        }
        //[Authorize(Roles = "Coordinator")]
        [HttpPut("delete/employmentContract")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Guid>> DeleteMedicalWorkerEmploymentContract([FromBody] DeleteMedicalWorkerEmploymentContractCommand request)
        {
            var response = await _mediator.Send(request);

            return Ok(response);
        }
        [HttpGet("GetMedicalWorkersAssignedToMedcialTeam")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IList<MedicalWorker>>> GetMedicalWorkersAssignedToMedcialTeam([FromQuery] GetMedicalWorkersAssignedToMedcialTeamByMedcialTeamIdQuery request)
        {
            var response = await _mediator.Send(request);

            return Ok(response);
        }
        [HttpGet("GetMedicalWorkerByUserId")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<MedicalWorker>> GetMedicalWorkerByUserId([FromQuery] GetMedicalWorkerByUserIdQuery request)
        {
            var response = await _mediator.Send(request);

            return Ok(response);
        }
    }
}
