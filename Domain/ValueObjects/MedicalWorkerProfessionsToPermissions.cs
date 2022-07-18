using Domain.DDDBlocks;
using Domain.ValueObjects;
using Domain.ValueObjects.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.MedicalWorkerProfessionsToPermission
{
    public class MedicalWorkerProfessionsToPermissions : ValueObject
    {
        public MedicalWorkerProfessionEnum MedicalWorkerProfession { get; }
        public MedicRole MedicRole { get; }
        public MedicalTeamType MedicalTeamType { get; }
        private MedicalWorkerProfessionsToPermissions()
        {
            //For Ef
        }
        public MedicalWorkerProfessionsToPermissions(MedicalWorkerProfessionEnum medicalWorkerProfession, MedicRole medicRole, MedicalTeamType medicalTeamType)
        {
            MedicalWorkerProfession = medicalWorkerProfession;
            MedicRole = medicRole;
            MedicalTeamType = medicalTeamType;
        }       

        public override IEnumerable<object> GetPropertiesToCompare()
        {
            yield return MedicalWorkerProfession;
            yield return MedicRole;
            yield return MedicalTeamType;
        }
    }
}
