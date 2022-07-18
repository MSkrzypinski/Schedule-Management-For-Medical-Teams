using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Entities.Rules.MedicalTeamRules
{
    public class MedicalTeamMustBeUnique
    {
        public static string Message = "Medical team must be unique";
        private static IGenericCounter<MedicalTeam> _medicalTeamCounter;
        public static void Check(string code, IGenericCounter<MedicalTeam> medicalTeamCounter)
        {
            _medicalTeamCounter = medicalTeamCounter;

            if (_medicalTeamCounter.Count(x=>x.InformationAboutTeam.Code.Equals(code)).Result > 0)
            {
                throw new ApplicationException(Message);
            }
        }
    }
}
