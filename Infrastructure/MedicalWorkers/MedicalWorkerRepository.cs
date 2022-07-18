using Application.Persistence;
using Domain.Entities;
using Domain.ValueObjects.Enums;
using Infrastructure.MedicalWorkerProfessionsToPermission;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.MedicalWorkers
{
    public class MedicalWorkerRepository : BaseRepository<MedicalWorker>, IMedicalWorkerRepository
    {
        public MedicalWorkerRepository(ScheduleManagementContext scheduleManagementContext) : base(scheduleManagementContext)
        {

        }
        public async Task<MedicalWorker> GetMedicalWorkerByIdIncludeAllPropertiesAsync(Guid id)
        {
            return await _scheduleManagementContext.MedicalWorkers
                .Include(x => x.MedicalWorkerProfessions) 
                .Include(x => x.Shifts)
                .Include(x=>x.User)
                .FirstOrDefaultAsync(x => x.Id.Equals(id));
            
        }
        public async Task<IEnumerable<MedicalWorkerProfessionsToPermissions>> GetPermissionToProfessionAsync(MedicalWorkerProfessionEnum medicalWorkerProfession )
        {
            var medicalworkerRoles = await _scheduleManagementContext.MedicalWorkerProfessionsToPermissions.Where(x=>x.MedicalWorkerProfession.Equals(medicalWorkerProfession)).ToListAsync();

            return medicalworkerRoles;
        }
    }
}
