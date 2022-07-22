using Application.Authorization;
using Application.Persistence;
using Domain.Entities;
using Domain.ValueObjects;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Schedules.CreateNewSchedule
{
    public class CreateNewScheduleCommandHandler : IRequestHandler<CreateNewScheduleCommand, Guid>
    {
        private readonly IScheduleRepository _scheduleRepository;
        private readonly IMedicalTeamRepository _medicalTeamRepository;
        private readonly IGenericCounter<Schedule> _genericCounter;
        private readonly IAuthorizationService _authorizationService;
        private readonly IUserExecusionContextAccessor _userExecusionContextAccessor;
        public CreateNewScheduleCommandHandler
            (IScheduleRepository scheduleRepository, 
            IMedicalTeamRepository medicalTeamRepository, 
            IGenericCounter<Schedule> genericCounter, 
            IAuthorizationService authorizationService, 
            IUserExecusionContextAccessor userExecusionContextAccessor)
        {
            _scheduleRepository = scheduleRepository;
            _medicalTeamRepository = medicalTeamRepository;
            _genericCounter = genericCounter;
            _authorizationService = authorizationService;
            _userExecusionContextAccessor = userExecusionContextAccessor;
        }

        public async Task<Guid> Handle(CreateNewScheduleCommand request, CancellationToken cancellationToken)
        {
            var medicalTeam = await _medicalTeamRepository.GetByIdAsync(request.MedicalTeamId);
            if (medicalTeam == null)
            {
                throw new ArgumentNullException("Invalid medical team");
            }

            var authorizationResult = _authorizationService.AuthorizeAsync
                (_userExecusionContextAccessor.User,medicalTeam, new MustBeCoordinatorForThisTeamRequirement()).Result;

            if (!authorizationResult.Succeeded)
            {
                throw new UnauthorizedAccessException("Authorization failed");
            }

            var schedule = Schedule.Create(medicalTeam, new MonthAndYearOfSchedule(request.YearOfSchedule, request.MonthOfSchedule), _genericCounter);
            await _scheduleRepository.AddAsync(schedule);

            return schedule.Id;
        }
    }
}
