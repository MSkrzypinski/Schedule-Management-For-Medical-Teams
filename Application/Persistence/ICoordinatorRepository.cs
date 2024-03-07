using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Persistence
{
    public interface ICoordinatorRepository : IBaseRepository<Coordinator>
    {
        Task<Coordinator> GetCoordinatorIncludeAllPropertiesAsync(Guid id);
        Task<Coordinator> GetCoordinatorIncludeAllPropertiesByUserIdAsync(Guid userId);
        Task<IEnumerable<MedicalTeam>> GetAllMedicalTeamsAssignedToCoordinatorByUserId(Guid userId);

    }
}
