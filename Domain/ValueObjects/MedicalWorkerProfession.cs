using Domain.DDDBlocks;
using Domain.ValueObjects.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json.Serialization;

namespace Domain.ValueObjects
{

    public class MedicalWorkerProfession : ValueObject
    {

        public MedicalWorkerProfessionEnum MedicalWorkerProfessionEnum { get;}

        public MedicalWorkerProfession(MedicalWorkerProfessionEnum medicalWorkerProfessionEnum)
        {
            MedicalWorkerProfessionEnum = medicalWorkerProfessionEnum;
        }
        private MedicalWorkerProfession()
        {
            //For Ef
        }
        internal static MedicalWorkerProfession Create(MedicalWorkerProfessionEnum medicalWorkerProfessionEnum)
        {
            return new MedicalWorkerProfession(medicalWorkerProfessionEnum);
        }
        public override IEnumerable<object> GetPropertiesToCompare()
        {
            yield return MedicalWorkerProfessionEnum;
        }
    }
}
