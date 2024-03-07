using Domain.DDDBlocks;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.ValueObjects
{
    public class MonthAndYearOfSchedule : ValueObject
    {
        public int Year { get; }
        public int Month { get; }
        private MonthAndYearOfSchedule()
        {
            //For Ef
        }
        public MonthAndYearOfSchedule(int year, int month)
        {
            if (month < 1 || month > 12)
                throw new ApplicationException("Month must be in range 1-12");
            if (year < DateTime.Now.Year || year > (DateTime.Now.Year + 1))
                throw new ApplicationException($"Year must be {DateTime.Now.Year} or {DateTime.Now.Year+1}");
            Year = year;
            Month = month;
        }
        public override IEnumerable<object> GetPropertiesToCompare()
        {
            yield return Year;
            yield return Month;
        }
    }
}
