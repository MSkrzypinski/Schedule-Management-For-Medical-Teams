using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Schedules.CreateNewSchedule
{
    public class CreateNewScheduleCommand : IRequest<Guid>
    {
        public Guid MedicalTeamId { get; set; }
        public int MonthOfSchedule { get; set; }
        public int YearOfSchedule { get; set; }
    }
}
