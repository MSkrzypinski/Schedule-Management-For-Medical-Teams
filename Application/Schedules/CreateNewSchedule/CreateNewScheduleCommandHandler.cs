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
    public class CreateNewScheduleCommandHandler : IRequestHandler<CreateNewScheduleCommand, CreateNewScheduleCommandResponse>
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

        public async Task<CreateNewScheduleCommandResponse> Handle(CreateNewScheduleCommand request, CancellationToken cancellationToken)
        {
            var medicalTeam = await _medicalTeamRepository.GetByIdAsync(request.MedicalTeamId);
            if (medicalTeam == null)
            {
                return new CreateNewScheduleCommandResponse("Medical team is invalid", false);
            }

            var authorizationResult = _authorizationService.AuthorizeAsync
                (_userExecusionContextAccessor.User,medicalTeam, new MustBeCoordinatorForThisTeamRequirement()).Result;

            if (!authorizationResult.Succeeded)
            {
                return new CreateNewScheduleCommandResponse("Authorization failed", false);
            }

            var schedule = Schedule.Create(medicalTeam, new MonthAndYearOfSchedule(request.YearOfSchedule, request.MonthOfSchedule), _genericCounter);
            await _scheduleRepository.AddAsync(schedule);

            return new CreateNewScheduleCommandResponse(schedule.Id,"Schedule has been created",true);
        }
    }
}
