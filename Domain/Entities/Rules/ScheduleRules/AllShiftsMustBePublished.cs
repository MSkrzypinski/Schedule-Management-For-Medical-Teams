using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Domain.Entities.Rules.ScheduleRules
{
    public class AllShiftsMustBePublished
    {
        public static string Message ="All shifts must be published";

        public static void Check(Schedule schedule)
        {
            if (!schedule.Shifts.All(x=>x.IsPublished == true))
            {
                throw new ApplicationException(Message);
            }
        }
    }
}
