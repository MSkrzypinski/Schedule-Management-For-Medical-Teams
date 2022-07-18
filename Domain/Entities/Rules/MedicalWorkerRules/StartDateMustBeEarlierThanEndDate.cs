using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.Rules.MedicalWorkerRules
{
    public class StartDateMustBeEarlierThanEndDate
    {
        public static string Message = "Start date must be earlier than end date";
        
        public static void Check(DateTime start,DateTime end)
        {
            if (start>end)
            {
                throw new ApplicationException(Message);
            }
        }
    }
}
