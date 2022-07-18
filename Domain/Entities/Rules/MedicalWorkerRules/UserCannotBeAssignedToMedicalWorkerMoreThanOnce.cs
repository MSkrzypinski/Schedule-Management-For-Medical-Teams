
using Domain.Entities.Rules.UserRules;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Domain.Entities.Rules.MedicalWorkerRules
{
    public class UserCannotBeAssignedToMedicalWorkerMoreThanOnce
    {
        public static string Message = "User is already assigned to other medical worker";
        private static IGenericCounter<MedicalWorker> _medicalWorkerCounter;
        public static void Check(User user, IGenericCounter<MedicalWorker> medicalWorkerCounter)
        {
            _medicalWorkerCounter = medicalWorkerCounter;
            
            if (_medicalWorkerCounter.Count(x=>x.User.Email.Equals(user.Email)).Result>0)
            {
                throw new ApplicationException(Message);
            }
        }

    }
}
