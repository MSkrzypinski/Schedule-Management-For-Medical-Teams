using Domain.DDDBlocks;
using Domain.Entities.Rules.MedicalWorkerRules;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.ValueObjects
{
    public class DayOff : ValueObject
    {
        public DateTime Start { get; }
        public DateTime End { get; private set; }
        private DayOff()
        {
            //For EF
        }
        public DayOff(DateTime start, DateTime end)
        {
            Start = start;
            End = end;
        }
        internal static DayOff Create(DateTime start, DateTime end)
        {
            StartDateMustNotBePastDate.Check(start);
            StartDateMustBeEarlierThanEndDate.Check(start, end);

            return new DayOff(start, end);
        }
        public bool Includes(DateTime start, DateTime end)
        {
            if (end < End)
                return (Start < start && End > start) || (Start < end && End > end);

            else if (Start.Equals(start) && End.Equals(end))
                return true;

            else
                return (start < Start && start > End) || (start < End && end >= End);
        }
        public override IEnumerable<object> GetPropertiesToCompare()
        {
            yield return Start;
            yield return End;
        }
    }
}
