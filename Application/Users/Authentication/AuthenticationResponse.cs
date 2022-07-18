using Application.Responses;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.User.Authentication
{
    public class AuthenticationResponse : BaseResponse
    {
        public string Email { get; set; }
        public string Token { get; set; }

        public AuthenticationResponse(string message = null) : base(message)
        {
        }

        public AuthenticationResponse(string message, bool success) : base(message, success)
        {
        }

        public AuthenticationResponse(ValidationResult validationResult) : base(validationResult)
        {
        }
        public AuthenticationResponse(string email,string token)
        {
            Email = email;
            Token = token;
            Success = true;
        }
    }
}
