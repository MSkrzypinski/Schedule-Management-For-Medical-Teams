using Application.Persistence;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Coordinators
{
    public class CoordinatorRepository : BaseRepository<Coordinator>, ICoordinatorRepository
    {
        public CoordinatorRepository(ScheduleManagementContext scheduleManagementContext) : base(scheduleManagementContext)
        {

        }

        public async Task<Coordinator> GetCoordinatorIncludeAllPropertiesAsync(Guid id)
        {
            return await _scheduleManagementContext.Coordinators
                .Include(x => x.User)
                .Include(x=>x.MedicalTeams)
                .FirstAsync(x => x.Id.Equals(id));
        }
        public async Task<Coordinator> GetCoordinatorIncludeAllPropertiesByUserIdAsync(Guid userId)
        {
            return await _scheduleManagementContext.Coordinators
                .Include(x => x.User)
                .Include(x => x.MedicalTeams)
                .FirstAsync(x => x.User.Id.Equals(userId));
        }
        public async Task<IEnumerable<MedicalTeam>> GetAllMedicalTeamsAssignedToCoordinatorByUserId(Guid userId)
        {
            return await _scheduleManagementContext.MedicalTeams
                .Include(x => x.InformationAboutTeam)
                .Include(x => x.Coordinator)
                .Include(x => x.Coordinator.User)
                .Where(x => x.Coordinator.User.Id == userId)
                .ToListAsync();
        }
    }
}
