
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.User.Authentication
{
    public class AuthenticationResponse
    {
        public string Email { get; set; }
        public string Token { get; set; }

        public AuthenticationResponse(string email,string token)
        {
            Email = email;
            Token = token;
        }
    }
}
