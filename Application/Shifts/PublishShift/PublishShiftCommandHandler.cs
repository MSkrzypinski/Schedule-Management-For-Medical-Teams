using Application.Authorization;
using Application.Persistence;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Shifts.PublishShift
{
    public class PublishShiftCommandHandler : IRequestHandler<PublishShiftCommand,Unit>
    {
        private readonly IShiftRepository _shiftRepository;
        private readonly IAuthorizationService _authorizationService;
        private readonly IUserExecusionContextAccessor _userExecusionContextAccessor;


        public PublishShiftCommandHandler(
            IShiftRepository shiftRepository, 
            IAuthorizationService authorizationService, 
            IUserExecusionContextAccessor userExecusionContextAccessor)
        {
            _shiftRepository = shiftRepository;
            _authorizationService = authorizationService;
            _userExecusionContextAccessor = userExecusionContextAccessor;
        }

        public async Task<Unit> Handle(PublishShiftCommand request, CancellationToken cancellationToken)
        {
            var shift = await _shiftRepository.GetShiftByIdIncludeAllPropertiesAsync(request.ShiftId);

            if (shift == null)
            {
                throw new ArgumentNullException("Invalid shift");
            }

            var authorizationResult = _authorizationService.AuthorizeAsync
              (_userExecusionContextAccessor.User, shift.MedicalTeam, new MustBeCoordinatorForThisTeamRequirement()).Result;

            if (!authorizationResult.Succeeded)
            {
                throw new UnauthorizedAccessException("Authorization failed");
            }

            shift.Publish();

            await _shiftRepository.UpdateAsync(shift);

            return Unit.Value;
        }
    }
}
