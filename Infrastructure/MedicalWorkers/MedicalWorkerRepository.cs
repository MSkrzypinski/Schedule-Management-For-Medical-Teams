using Application.Persistence;
using Domain.Entities;
using Domain.ValueObjects;
using Domain.ValueObjects.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.MedicalWorkers
{
    public class MedicalWorkerRepository : BaseRepository<MedicalWorker>, IMedicalWorkerRepository
    {
        public MedicalWorkerRepository(ScheduleManagementContext scheduleManagementContext) : base(scheduleManagementContext)
        {

        }

        public async Task<IEnumerable<MedicalWorker>> GetMedicalWorkersAssignedToCoordinatorByCoordinatorIdAsync(Guid coordinatorId)
        {
            return await _scheduleManagementContext.MedicalWorkers
                .Include(x => x.MedicalWorkerProfessions)
                .Include(x=>x.User)
                .Include(x=> x.EmploymentContracts)
                .ThenInclude(x=>x.MedicalTeam.InformationAboutTeam)
                .Where(x => x.EmploymentContracts.Any(ec => ec.MedicalTeam.Coordinator.Id == coordinatorId) 
                    || x.EmploymentContracts.Count() == 0)
                .ToListAsync();
        }

        public async Task<MedicalWorker> GetMedicalWorkerByIdIncludeAllPropertiesAsync(Guid id)
        {
            return await _scheduleManagementContext.MedicalWorkers
                .AsSplitQuery()
                .Where(x => x.Id.Equals(id))
                .Include(x => x.Shifts)
                .Include(x=>x.EmploymentContracts)
                .ThenInclude(x=>x.MedicalTeam)
                .FirstOrDefaultAsync(x => x.Id.Equals(id));
        }
        public async Task<MedicalWorker> GetMedicalWorkerByUserIdIncludeAllPropertiesAsync(Guid id)
        {
            return await _scheduleManagementContext.MedicalWorkers
                .Include(x => x.Shifts)
                .ThenInclude(x=>x.Driver)
                .Include(x => x.Shifts)
                .ThenInclude(x=>x.Manager)
                .Include(x => x.Shifts)
                .ThenInclude(x=>x.CrewMember)
                .FirstOrDefaultAsync(x => x.User.Id.Equals(id));
        }
        public async Task<IEnumerable<MedicalWorkerProfessionsToPermissions>> GetPermissionToProfessionAsync(MedicalWorkerProfessionEnum medicalWorkerProfession )
        {
            return await _scheduleManagementContext.MedicalWorkerProfessionsToPermissions
                .Where(x=>x.MedicalWorkerProfession.Equals(medicalWorkerProfession))
                .ToListAsync();
        }
        public async Task<IList<MedicalWorker>> GetMedicalWorkersAssignedToMedcialTeamByMedcialTeamIdAsync(Guid medicalTeamId,MedicRole medicRole)
        {
            return await _scheduleManagementContext.MedicalWorkers
                .AsSplitQuery()
                .Include(x=>x.User)
                .Include(x=>x.DaysOff)
                .Include(x=>x.Shifts)
                .Where(x => x.EmploymentContracts.Any(ec => ec.MedicalTeam.Id.Equals(medicalTeamId) 
                    && ec.MedicRole.Equals(medicRole)))
                .ToListAsync();
        }
    }
}
