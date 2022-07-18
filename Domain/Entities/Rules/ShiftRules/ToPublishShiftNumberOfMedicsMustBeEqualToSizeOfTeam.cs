using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.Rules.ShiftRules
{
    public class ToPublishShiftNumberOfMedicsMustBeEqualToSizeOfTeam
    {
        public static string Message = "To publish number of medics must be equal to size of team";
        public static void Check(Shift shift)
        {
            if (shift.GetQuantityOfMedics() != shift.MedicalTeam.InformationAboutTeam.SizeOfTeam)
                throw new ApplicationException(Message);
        }
    }
}
