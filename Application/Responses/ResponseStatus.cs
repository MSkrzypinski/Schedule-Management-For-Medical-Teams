using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Responses
{
    public enum ResponseStatus
    {
        Success = 0,
        NotFound = 1,
        BadQuery = 2,
        ValidationError = 3
    }
}
