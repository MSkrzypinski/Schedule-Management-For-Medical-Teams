using Application.Persistence;
using Domain.Entities;

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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
                .ThenInclude(x=>x.InformationAboutTeam)
                .Include(x=>x.Crew)
                .Include(x=>x.Schedule)
                .FirstOrDefaultAsync(x => x.Id.Equals(id));
        }
    }
}
