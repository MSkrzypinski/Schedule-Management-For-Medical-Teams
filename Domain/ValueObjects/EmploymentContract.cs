using Domain.DDDBlocks;
using Domain.Entities.Rules.EmploymentContractRules;
using Domain.ValueObjects;
using Domain.ValueObjects.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class EmploymentContract : ValueObject
    {
        public ContractType ContractType { get; }
        public MedicalWorkerProfessionEnum MedicalWorkerProfession {get;}
        public MedicRole MedicRole { get; }
        public MedicalTeam MedicalTeam { get; }
        public MedicalWorker MedicalWorker { get; }
        private EmploymentContract()
        {
            //For EF
        }
        private EmploymentContract(MedicalWorker medicalWorker, MedicalTeam medicalTeam, ContractType contractType, MedicRole medicRole, MedicalWorkerProfessionEnum medicalWorkerProfession)
        {
            MedicalWorker = medicalWorker;
            ContractType = contractType;
            MedicalTeam = medicalTeam;
            MedicRole = medicRole;
            MedicalWorkerProfession = medicalWorkerProfession;
          
        }
        internal static EmploymentContract Create(MedicalWorker medicalWorker, MedicalTeam medicalTeam, ContractType contractType, MedicRole medicRole, MedicalWorkerProfessionEnum medicalWorkerProfession)
        {
            if (medicalWorker == null)
                throw new ArgumentException("Medical worker cannot be null");
            if (medicalTeam == null)
                throw new ArgumentException("Medical team cannot be null");     

            return new EmploymentContract(medicalWorker, medicalTeam, contractType, medicRole,medicalWorkerProfession);
        }

        public override IEnumerable<object> GetPropertiesToCompare()
        {
            yield return ContractType;
            yield return MedicalWorkerProfession;
            yield return MedicRole;
            yield return MedicalTeam;
            yield return MedicalWorker;
        }
    }
}
