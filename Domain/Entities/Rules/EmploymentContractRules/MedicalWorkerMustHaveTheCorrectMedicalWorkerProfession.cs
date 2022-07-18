using Domain.ValueObjects;
using Domain.ValueObjects.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Entities.Rules.EmploymentContractRules
{
    public class MedicalWorkerMustHaveTheCorrectMedicalWorkerProfession
    {
        public static string Message = "Medical worker must have the correct medical worker type";
        public static void Check(MedicalWorker medicalWorker, MedicalWorkerProfession medicalWorkerProfession)
        {
            if (!medicalWorker.MedicalWorkerProfessions.Any(x => x.Equals(medicalWorkerProfession)))
                throw new ApplicationException(Message);
        }
    }
}
