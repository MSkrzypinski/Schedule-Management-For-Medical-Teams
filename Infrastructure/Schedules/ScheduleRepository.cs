using Application.Persistence;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
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
        public async Task<Schedule> GetScheduleByIdIncludeAllPropertiesAsync(Guid id)
        {
            return await _scheduleManagementContext.Schedules
                .Include(x=>x.Shifts)
                .ThenInclude(x => x.MedicalTeam)
                .ThenInclude(x => x.InformationAboutTeam)
                .Include(x=>x.Shifts)
                .ThenInclude(x => x.Crew)
                .Include(x=>x.Shifts)
                .ThenInclude(x => x.Schedule)
                .FirstOrDefaultAsync(x => x.Id.Equals(id));
        }
    }
}
