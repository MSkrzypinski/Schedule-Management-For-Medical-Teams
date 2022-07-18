using Domain.DDDBlocks;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Domain.ValueObjects
{
    public class Name : ValueObject
    {
        public string FirstName { get; }
        public string LastName { get; }
        private Name()
        {
            //For Ef
        }
        public Name(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }
     
        public override IEnumerable<object> GetPropertiesToCompare()
        {
            yield return FirstName;
            yield return LastName;
        }
    }
}
