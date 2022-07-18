using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Users.RegisterNewUser
{
    public class RegisterNewUserValidator : AbstractValidator<RegisterNewUserCommand>
    {
        public RegisterNewUserValidator()
        {
            RuleFor(x => x.Email.Value)
                .EmailAddress()
                .NotEmpty();

            RuleFor(x => x.Name.FirstName)
                .MinimumLength(2)
                .MaximumLength(20)
                .NotEmpty();

            RuleFor(x => x.Name.LastName)
                .MinimumLength(2)
                .MaximumLength(20)
                .NotEmpty();

            RuleFor(x => x.PhoneNumber.Number)
                .Length(9)
                .NotEmpty();

            RuleFor(x => x.Password.Value)
                .NotEmpty()
                .MinimumLength(5)
                .MaximumLength(20);
        }
    }
}
