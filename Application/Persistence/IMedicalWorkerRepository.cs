using Domain.Entities;
using Domain.ValueObjects.Enums;
using Infrastructure.MedicalWorkerProfessionsToPermission;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Persistence
{
    public interface IMedicalWorkerRepository : IBaseRepository<MedicalWorker>
    {
        Task<MedicalWorker> GetMedicalWorkerByIdIncludeAllPropertiesAsync(Guid id);
        Task<IEnumerable<MedicalWorkerProfessionsToPermissions>> GetPermissionToProfessionAsync(MedicalWorkerProfessionEnum medicalWorkerProfession);
    }
}
