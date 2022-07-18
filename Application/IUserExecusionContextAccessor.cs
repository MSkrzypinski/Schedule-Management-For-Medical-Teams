using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace Application
{
    public interface IUserExecusionContextAccessor
    {
        Guid UserId { get; }
        ClaimsPrincipal User { get; }
    }
}
