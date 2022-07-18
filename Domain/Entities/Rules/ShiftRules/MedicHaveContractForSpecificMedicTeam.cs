using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Domain.ValueObjects.Enums;

namespace Domain.Entities.Rules.ShiftRules
{
    public static class MedicHaveContractForSpecificMedicTeam
    {
        public static string Message = "Medic have not a contract for this team";
        public static void Check(MedicalWorker medicalWorker, MedicalTeam medicalTeam, MedicRole medicRole)
        {
            if (!medicalWorker.EmploymentContracts.Any(x => x.MedicalTeam.InformationAboutTeam.Equals(medicalTeam.InformationAboutTeam) 
                && x.MedicRole == medicRole))
            {
                throw new ApplicationException(Message);
            }
        }
    }
}
