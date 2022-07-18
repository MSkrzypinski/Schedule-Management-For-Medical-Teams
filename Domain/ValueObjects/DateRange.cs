using Domain.DDDBlocks;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.ValueObjects
{
    public class DateRange : ValueObject
    {
        public DateTime Start { get; }
        public DateTime End { get; private set; }

        private DateRange()
        {
            //For EF
        }

        public DateRange(DateTime start, DateTime end)
        {
            Start = start;
            End = end;
        }

        public bool Includes(DateRange range)
        {
            if (range.End < End)
                return (Start < range.Start && End > range.Start) || (Start < range.End && End > range.End);

            else if (range.Equals(this))
                return true;

            else
                return (range.Start < Start && range.Start > End) || (range.Start < End && range.End >= End);
        }
        public override IEnumerable<object> GetPropertiesToCompare()
        {
            yield return Start;
            yield return End;
        }

    }
}
