using Application.Persistence;
using Domain.Entities;

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Schedules
{
    public class ScheduleRepository : BaseRepository<Schedule>, IScheduleRepository
    {
        public ScheduleRepository(ScheduleManagementContext scheduleManagementContext) : base(scheduleManagementContext)
        {

        }
    }
}
