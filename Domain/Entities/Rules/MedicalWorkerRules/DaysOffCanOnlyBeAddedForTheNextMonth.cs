﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.Rules.MedicalWorkerRules
{
    public class DaysOffCanOnlyBeAddedForTheNextMonth
    {
        public static string Message = "Days off can only be added for the next month";

        public static void Check(DateTime start,DateTime end)
        {
            if ((start.Month != DateTime.Now.Month+1 || end.Month != DateTime.Now.Month+1))
            {
                throw new ApplicationException(Message);
            }
        }
    }
}