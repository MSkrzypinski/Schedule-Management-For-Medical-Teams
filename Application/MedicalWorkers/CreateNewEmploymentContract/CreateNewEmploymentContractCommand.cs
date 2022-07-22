using Domain.ValueObjects.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.MedicalWorkers.CreateNewEmploymentContract
{
    public class CreateNewEmploymentContractCommand : IRequest<Unit>
    {
        public Guid MedicalWorkerId { get; set; }
        public Guid MedicalTeamId { get; set; }
        public MedicRole MedicRole { get; set; }
        public ContractType ContractType { get; set; }
        public MedicalWorkerProfessionEnum MedicalWorkerProfession { get; set; }
    }
}
