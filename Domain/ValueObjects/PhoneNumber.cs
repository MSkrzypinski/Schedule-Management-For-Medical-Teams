using Domain.DDDBlocks;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Domain.ValueObjects
{
    public class PhoneNumber : ValueObject
    {
        public string Number { get; }
        private PhoneNumber()
        {
            //For Ef
        }
        public PhoneNumber(string number)
        {
            Number = number;
        }
      
        public override IEnumerable<object> GetPropertiesToCompare()
        {
            yield return Number;
        }
    }
}
