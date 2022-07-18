using Application.Responses;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Users.RegisterNewUser
{
    public class RegisterNewUserCommandResponse : BaseResponse
    {
        public Guid Id { get; set; }
        public RegisterNewUserCommandResponse()
        {
        }

        public RegisterNewUserCommandResponse(string message = null) : base(message)
        {
        }

        public RegisterNewUserCommandResponse(ValidationResult validationResult) : base(validationResult)
        {
        }

        public RegisterNewUserCommandResponse(string message, bool success) : base(message, success)
        {
        }
        public RegisterNewUserCommandResponse(Guid id)
        {
            Id = id;
            Success = true;
        }
    }
}
