using Application.Mapper.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Users.RegisterNewUser
{
    public class RegisterNewUserCommand : IRequest<Guid>
    {
        public NameDto Name { get; set; }
        public PasswordDto Password { get; set; }
        public PhoneNumberDto PhoneNumber { get; set; }
        public EmailDto Email { get; set; }
    }
}
