
using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Domain.Entities.Rules.UserRules
{
    public class UserMustBeUnique
    {
        public static string Message = "User must be unique";
        private static IGenericCounter<User> _userCounter;
        public static void Check(Email email, IGenericCounter<User> userCounter)
        {
            _userCounter = userCounter;
      
            if (_userCounter.Count(x=>x.Email.Equals(email)).Result>0)
            {
                throw new BusinessRuleException(Message);
            }
        }
    }
}
