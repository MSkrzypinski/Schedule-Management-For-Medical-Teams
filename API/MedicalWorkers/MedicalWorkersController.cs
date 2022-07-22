using API.MedicalWorkers.Requests;
using Application.MedicalWorkers.AddDayOff;
using Application.MedicalWorkers.AddMedicalWorkerProfession;
using Application.MedicalWorkers.CreateNewEmploymentContract;
using Application.MedicalWorkers.CreateNewMedicalWorker;
using Domain.ValueObjects.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

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
        public async Task<ActionResult<Guid>> CreateNewMedicalWorker([FromBody] CreateNewMedicalWorkerRequest request)
        {
            var response = await _mediator.Send(new CreateNewMedicalWorkerCommand() 
            {
                DateOfBirth=request.DateOfBirth,
                Address=request.Address,
                UserId = request.UserId
            });

            return Ok(response);
        }

        [Authorize(Roles = "Coordinator")]
        [HttpPost("AddMedicalWorkerProfession")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Unit>> AddMedicalWorkerProfession([FromBody] AddMedicalWorkerProfessionRequest request,[FromQuery] MedicalWorkerProfessionEnum medicalWorkerProfessionEnum)
        {
            var response = await _mediator.Send(new AddMedicalWorkerProfessionCommand()
            {
                MedicalWorkerId = request.MedicalWorkerId,
                MedicalWorkerProfessionEnum = medicalWorkerProfessionEnum
            }); 

            return Ok(response);
        }

        [Authorize(Roles = "Coordinator")]
        [HttpPost("CreateNewEmploymentContract")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Unit>> CreateNewEmploymentContract([FromBody] AddEmploymentContractRequest request, [FromQuery] ContractType contractType, MedicRole medicRole,MedicalWorkerProfessionEnum medicalWorkerProfession )
        {
            var response = await _mediator.Send(new CreateNewEmploymentContractCommand()
            {
                MedicalTeamId=request.MedicalTeamId,
                MedicalWorkerId = request.MedicalWorkerId,
                MedicRole=medicRole,
                ContractType = contractType,
                MedicalWorkerProfession = medicalWorkerProfession
            });

            return Ok(response);
        }
        [Authorize(Roles = "MedicalWorker")]
        [HttpPost("AddDayOff")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Unit>> AddDayOff([FromBody] AddDayOffCommand addDayOffCommand)
        {
            var response = await _mediator.Send(addDayOffCommand);

            return Ok(response);
        }
    }
}
