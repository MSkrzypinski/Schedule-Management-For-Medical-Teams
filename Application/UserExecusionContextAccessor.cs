using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Application
{
    public class UserExecusionContextAccessor : IUserExecusionContextAccessor
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserExecusionContextAccessor(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public Guid UserId 
        {
            get
            {
                if (_httpContextAccessor.HttpContext?.User == null)
                {
                    throw new ApplicationException("User context is not available");
                }
                return Guid.Parse(_httpContextAccessor.HttpContext.User.Claims.Single(x => x.Type == ClaimTypes.NameIdentifier).Value);
            }
        }
        public ClaimsPrincipal User
        {
            get
            {
                if (_httpContextAccessor.HttpContext?.User == null)
                {
                    throw new ApplicationException("User context is not available");
                }
                return _httpContextAccessor.HttpContext.User;
            }
        }
    }
}
