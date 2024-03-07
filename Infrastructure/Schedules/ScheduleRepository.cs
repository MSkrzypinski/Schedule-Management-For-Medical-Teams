using Application.Persistence;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Schedules
{
    public class ScheduleRepository : BaseRepository<Schedule>, IScheduleRepository
    {
        public ScheduleRepository(ScheduleManagementContext scheduleManagementContext) : base(scheduleManagementContext)
        {
           
        }
        public async Task<Schedule> GetScheduleByMonthAndYearAndMedicalTeamIdIncludeAllPropertiesAsync(Guid medicalTeamId,int month,int year)
        {
            IQueryable<Schedule> query = _scheduleManagementContext.Schedules
                .AsNoTracking()
                .AsSplitQuery()
                .Include(x=>x.Shifts)
                .ThenInclude(x=>x.Driver)
                .ThenInclude(x=>x.User)
                .Include(x=>x.Shifts)
                .ThenInclude(x=>x.Manager)
                .ThenInclude(x=>x.User)
                .Include(x=>x.Shifts)
                .ThenInclude(x => x.CrewMember)
                .ThenInclude(x=>x.User)
                .Include(x=>x.MedicalTeam)
                .Where(x => x.MonthAndYearOfSchedule.Year == year 
                    && x.MonthAndYearOfSchedule.Month == month
                    && x.MedicalTeam.Id.Equals(medicalTeamId)).AsQueryable();
            return await query.SingleAsync();
        }
        
    }
}
