using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Domain.Entities.Rules.ShiftRules
{
    public static class MedicCanWorkInThisTerm
    {
        public static string Message = "Medic must not work in this term";
        public static void Check(MedicalWorker medicalWorker,Shift shift)
        {
            if(medicalWorker.Shifts.Any(x=>x.DateRange.Includes(shift.DateRange)) 
                || medicalWorker.DaysOff.Any(x=>x.Includes(shift.DateRange.Start,shift.DateRange.End)))
            {
                throw new ApplicationException(Message);
            }
        }
    }
}
