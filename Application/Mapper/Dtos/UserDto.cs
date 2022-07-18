using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace Application.Mapper.Dtos
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public NameDto Name { get; set; }
        public PasswordDto Password { get; set; }
        public PhoneNumberDto PhoneNumber { get; set; }
        public EmailDto Email { get; set; }
        public List<UserRole> UserRoles { get; set; }
    }
}
