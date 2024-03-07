using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.ValueObjects;
using Domain.ValueObjects.Enums;
using MediatR;

namespace Application.MedicalWorkers.GetMedicRoleToMedicalTeam
{
    public class GetMedicalWorkerProfessionToMedicalTeamQuery : IRequest<IList<MedicalWorkerProfessionsToPermissions>>
    {
        public Guid MedicalTeamId { get; set; }
        public MedicalWorkerProfessionEnum MedicalWorkerProfessionEnum{ get; set; }
    }
}