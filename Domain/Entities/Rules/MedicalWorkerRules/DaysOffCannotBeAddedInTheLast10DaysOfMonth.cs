using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.Rules.MedicalWorkerRules
{
    public class DaysOffCannotBeAddedInTheLast10DaysOfMonth
    {
        public static string Message = "Days off cannot be add in the last 10 days of month";
        public static DateTime dateTime = DateTime.Now;
        public static void Check()
        {
            if (DateTime.DaysInMonth(dateTime.Year,dateTime.Month)-10 <= dateTime.Day)
            {
                throw new ApplicationException(Message);
            }
        }
    }
}
