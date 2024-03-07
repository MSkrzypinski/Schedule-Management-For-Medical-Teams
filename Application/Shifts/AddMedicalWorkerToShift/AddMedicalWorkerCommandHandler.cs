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

namespace Application.Shifts.AddMedicalWorkerToShift
{
    public class AddMedicalWorkerCommandHandler : IRequestHandler<AddMedicalWorkerCommand, Unit>
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

        public async Task<Unit> Handle(AddMedicalWorkerCommand request, CancellationToken cancellationToken)
        {
            var medicalWorker = await _medicalWorkerRepository.GetMedicalWorkerByIdIncludeAllPropertiesAsync(request.MedicalWorkerId);
            if (medicalWorker == null)
            {
                throw new ArgumentNullException("Invalid medical worker");
            }

            var shift = await _shiftRepository.GetShiftByIdIncludeAllPropertiesAsync(request.ShiftId);
            if (shift == null)
            {
                throw new ArgumentNullException("Invalid shift");
            }

            if (request.MedicRole.Equals(MedicRole.Driver))
                shift.AddOrChangeDriver(medicalWorker);

            else if (request.MedicRole.Equals(MedicRole.Manager))
                shift.AddOrChangeCrewManager(medicalWorker);
            else
                shift.AddCrewMember(medicalWorker);

            await _shiftRepository.UpdateAsync(shift);

            return Unit.Value;
        }
    }
}
