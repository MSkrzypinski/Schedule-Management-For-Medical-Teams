using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Persistence;
using Domain.Entities;
using MediatR;

namespace Application.Schedules.GetScheduleByMonthAndYearAndMedicalTeamId
{
    public class GetScheduleByMonthAndYearAndMedicalTeamIdQueryHandler : IRequestHandler<GetScheduleByMonthAndYearAndMedicalTeamIdQuery, Schedule>
    {
        private readonly IScheduleRepository _scheduleRepository;

        public GetScheduleByMonthAndYearAndMedicalTeamIdQueryHandler(IScheduleRepository scheduleRepository)
        {
            _scheduleRepository = scheduleRepository;
        }

        public async Task<Schedule> Handle(GetScheduleByMonthAndYearAndMedicalTeamIdQuery request, CancellationToken cancellationToken)
        {
            return await _scheduleRepository.GetScheduleByMonthAndYearAndMedicalTeamIdIncludeAllPropertiesAsync(request.MedicalTeamId,request.Month,request.Year);
        }
    }

}