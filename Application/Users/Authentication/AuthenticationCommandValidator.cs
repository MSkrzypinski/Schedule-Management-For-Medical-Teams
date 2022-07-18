using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;

namespace Application.User.Authentication
{
    public class AuthenticationCommandValidator : AbstractValidator<AuthenticationCommand>
    {
        public AuthenticationCommandValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(x => x.Password)
                .NotEmpty()
                .MinimumLength(5)
                .MaximumLength(20);
        }
    }
}
