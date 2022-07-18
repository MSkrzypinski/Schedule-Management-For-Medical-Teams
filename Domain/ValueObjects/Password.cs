using Domain.DDDBlocks;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.ValueObjects
{
    public class Password : ValueObject
    {
        public string Value { get; }
        private Password()
        {
            //For Ef
        }
        public Password(string value)
        {
            Value = value;
        }

        public override IEnumerable<object> GetPropertiesToCompare()
        {
            yield return Value;
        }
    }
}
