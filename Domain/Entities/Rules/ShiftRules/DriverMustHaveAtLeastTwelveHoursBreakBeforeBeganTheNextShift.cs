using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Domain.ValueObjects.Enums;

namespace Domain.Entities.Rules.ShiftRules
{
    public static class DriverMustHaveAtLeastTwelveHoursBreakBeforeBeganTheNextShift
    {
        public static string Message = "Driver must have 12 hours break between shifts";
        public static void Check(MedicalWorker driver,Shift shift)
        {
            if (driver.Shifts.FindAll(x=>x.Driver!=null).Any(x =>x.Driver.Id.Equals(driver.Id) 
                && ((Math.Abs((x.DateRange.Start - shift.DateRange.End).TotalHours) < 12) 
                || (Math.Abs((x.DateRange.End - shift.DateRange.Start).TotalHours) < 12))))
            {
                throw new ApplicationException(Message);
            }
        }
    }
}
