using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.Rules.MedicalWorkerRules
{
    public class StartDateMustNotBePastDate
    {
        public static string Message = "Start date mustn't be past date";

        public static void Check(DateTime start)
        {
            if (start < DateTime.Now)
            {
                throw new ApplicationException(Message);
            }
        }
    }
}
