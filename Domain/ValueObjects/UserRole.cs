using Domain.DDDBlocks;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.ValueObjects
{
    public class UserRole : ValueObject
    {
        public string Value { get; }
        private UserRole()
        {
            //For Ef
        }
        public UserRole(string value)
        {
            Value = value;
        }
        public static UserRole Coordinator()
        {
            return new UserRole(nameof(Coordinator));
        }
        public static UserRole MedicalWorker()
        {
            return new UserRole(nameof(MedicalWorker));
        }
       
        public override IEnumerable<object> GetPropertiesToCompare()
        {
            yield return Value;
        }
    }
}
