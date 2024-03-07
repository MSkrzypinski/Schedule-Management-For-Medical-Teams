using Application.Mapper.Dtos;
using Application.Persistence;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Shifts
{
    public class ShiftRepository : BaseRepository<Shift>, IShiftRepository
    {
        public ShiftRepository(ScheduleManagementContext scheduleManagementContext) : base(scheduleManagementContext)
        {
            
        }
        public async Task<Shift> GetShiftByIdIncludeAllPropertiesAsync(Guid id)
        {
            return await _scheduleManagementContext.Shifts
                .Include(x=>x.MedicalTeam)
                .Include(x=>x.Crew)
                .Include(x=>x.Schedule)
                .FirstOrDefaultAsync(x => x.Id.Equals(id));
        }
        
        public async Task<IList<Shift>> GetShiftsByScheduleIdAsync(Guid scheduleId)
        {
            return await _scheduleManagementContext.Shifts
                .AsNoTracking()
                .AsSplitQuery()
                .Include(x=>x.Driver)
                .ThenInclude(x=>x.User)
                .Include(x=>x.Manager)
                .ThenInclude(x=>x.User)
                .Include(x=>x.CrewMember)
                .ThenInclude(x=>x.User)
                .Where(x => x.Schedule.Id.Equals(scheduleId)).ToListAsync();
        }
       public async Task<IList<Shift>> GetShiftsByMonthAndYearAndUserIdIncludeAllPropertiesAsync(Guid userId,int month,int year)
        {
            IQueryable<Shift> query = _scheduleManagementContext.Shifts
                .AsNoTracking()
                .AsSplitQuery()
                .Include(x=>x.Driver)
                .ThenInclude(x=>x.User)
                .Include(x=>x.Manager)
                .ThenInclude(x=>x.User)
                .Include(x => x.CrewMember)
                .ThenInclude(x=>x.User)
                .Include(x=>x.MedicalTeam)
                .Where(x => x.Schedule.MonthAndYearOfSchedule.Year == year 
                    && x.Schedule.MonthAndYearOfSchedule.Month == month &&
                    (x.CrewMember.User.Id.Equals(userId)
                    || x.Driver.User.Id.Equals(userId)
                    || x.Manager.User.Id.Equals(userId))).AsQueryable();
            return await query.ToListAsync();
        }
        
    }
}
