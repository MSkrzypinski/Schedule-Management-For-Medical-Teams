using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using MediatR;

namespace Application.Schedules.GetScheduleByMonthAndYearAndMedicalTeamId
{
    public class GetScheduleByMonthAndYearAndMedicalTeamIdQuery : IRequest<Schedule>
    {
        public Guid MedicalTeamId {get;set;}
        public int Year {get;set;}
        public int Month {get;set;} 
        
    }
}