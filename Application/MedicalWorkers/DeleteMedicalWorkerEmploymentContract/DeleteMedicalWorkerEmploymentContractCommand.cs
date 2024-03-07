using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace Application.MedicalWorkers.DeleteMedicalWorkerEmploymentContract
{
    public class DeleteMedicalWorkerEmploymentContractCommand : IRequest<Unit>  
    {
        public Guid MedicalWorkerId { get; set; }
        public Guid MedicalTeamId { get; set; }
        public string ContractType { get; set; }
        public string MedicalWorkerProfession { get; set; }
        public string MedicRole { get; set; }
    }
}