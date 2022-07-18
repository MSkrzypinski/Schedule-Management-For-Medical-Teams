using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.Rules.MedicalTeamRules
{
    public class SizeOfTeamMustBeGreaterThanOne
    {
        public static string Message = "Size of team must be greater than one";
        public static void Check(int sizeOfTeam)
        {
            if (sizeOfTeam < 2)
            {
                throw new ApplicationException(Message);
            }
        }
            
    }
}
