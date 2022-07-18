using Domain.DDDBlocks;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;

namespace Domain.ValueObjects
{
    public class Email : ValueObject
    {
        public string Value { get; }
        private Email()
        {
            //For EF
        }
        public Email(string value)
        {
            Value = value;
        }
       
        public override IEnumerable<object> GetPropertiesToCompare()
        {
            yield return Value;
        }
    }
}
