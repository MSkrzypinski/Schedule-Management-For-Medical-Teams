using Domain.Entities;
using Domain.ValueObjects;
using Domain.ValueObjects.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Persistence
{
    public interface IMedicalWorkerRepository : IBaseRepository<MedicalWorker>
    {
        Task<MedicalWorker> GetMedicalWorkerByIdIncludeAllPropertiesAsync(Guid id);
        Task<MedicalWorker> GetMedicalWorkerByUserIdIncludeAllPropertiesAsync(Guid id);
        Task<IEnumerable<MedicalWorker>> GetMedicalWorkersAssignedToCoordinatorByCoordinatorIdAsync(Guid coordinatorId);
        Task<IEnumerable<MedicalWorkerProfessionsToPermissions>> GetPermissionToProfessionAsync(MedicalWorkerProfessionEnum medicalWorkerProfession);
        Task<IList<MedicalWorker>> GetMedicalWorkersAssignedToMedcialTeamByMedcialTeamIdAsync(Guid medicalTeamId,MedicRole medicRole);
    }
}
