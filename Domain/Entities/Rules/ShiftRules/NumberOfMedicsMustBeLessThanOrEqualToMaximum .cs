using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.Rules.ShiftRules
{
    public class NumberOfMedicsMustBeLessThanOrEqualToMaximum
    {
        public static string Message ="Number of medics must be less than or equal to maximum";

        public static void Check(MedicalTeam medicalTeam, int quantity)
        {
            if (quantity > medicalTeam.InformationAboutTeam.SizeOfTeam)
            {
                throw new ApplicationException(Message);
            }
        }
    }
}
