using Application.Authorization;
using Application.Persistence;
using Application.Schedules.CreateNewSchedule;
using Domain.ValueObjects.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Shifts.AddDriver
{
    public class AddMedicalWorkerCommandHandler : IRequestHandler<AddMedicalWorkerCommand, AddMedicalWorkerCommandResponse>
    {
        private readonly IMedicalWorkerRepository _medicalWorkerRepository;
        private readonly IShiftRepository _shiftRepository;
        private readonly IAuthorizationService _authorizationService;
        private readonly IUserExecusionContextAccessor _userExecusionContextAccessor;

        public AddMedicalWorkerCommandHandler
            (IMedicalWorkerRepository medicalWorkerRepository, 
            IShiftRepository shiftRepository, 
            IAuthorizationService authorizationService, 
            IUserExecusionContextAccessor userExecusionContextAccessor)
        {
            _medicalWorkerRepository = medicalWorkerRepository;
            _shiftRepository = shiftRepository;
            _authorizationService = authorizationService;
            _userExecusionContextAccessor = userExecusionContextAccessor;
        }

        public async Task<AddMedicalWorkerCommandResponse> Handle(AddMedicalWorkerCommand request, CancellationToken cancellationToken)
        {
            var medicalWorker = await _medicalWorkerRepository.GetMedicalWorkerByIdIncludeAllPropertiesAsync(request.MedicalWorkerId);
            if (medicalWorker == null)
            {
                return new AddMedicalWorkerCommandResponse("Medical worker is invalid", false);
            }

            var shift = await _shiftRepository.GetShiftByIdIncludeAllPropertiesAsync(request.ShiftId);
            if (shift == null)
            {
                return new AddMedicalWorkerCommandResponse("Shift is invalid", false);
            }

            var authorizationResult = _authorizationService.AuthorizeAsync
                (_userExecusionContextAccessor.User, shift.MedicalTeam, new MustBeCoordinatorForThisTeamRequirement()).Result;

            if (!authorizationResult.Succeeded)
            {
                return new AddMedicalWorkerCommandResponse("Authorization failed", false);
            }

            if (request.MedicRole.Equals(MedicRole.Driver))
                shift.AddOrChangeDriver(medicalWorker);
            else if (request.MedicRole.Equals(MedicRole.Manager))
                shift.AddOrChangeDriver(medicalWorker);
            else
                shift.AddCrewMember(medicalWorker);

            await _shiftRepository.UpdateAsync(shift);

            return new AddMedicalWorkerCommandResponse($"{request.MedicRole} has been added", true);
        }
    }
}
