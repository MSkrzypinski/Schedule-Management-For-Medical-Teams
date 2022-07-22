using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class BusinessRuleException : Exception
    {
        public BusinessRuleException(string message) : base(message)
        {

        }

    }
}
