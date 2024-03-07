using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.ValueObjects.Enums;
using MediatR;

namespace Application.MedicalWorkers.GetMedicalWorkersAssignedToMedcialTeamByMedcialTeamId
{
    public class GetMedicalWorkersAssignedToMedcialTeamByMedcialTeamIdQuery : IRequest<IList<MedicalWorker>>
    {
        public Guid MedicalTeamId {get;set;}
        public MedicRole MedicRole {get;set;}  
        public Guid ShiftId{get;set;}
    }
}