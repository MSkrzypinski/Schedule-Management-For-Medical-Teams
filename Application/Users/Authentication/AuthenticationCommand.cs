using System;
using System.Collections.Generic;
using System.Text;
using Application.Mapper.Dtos;
using Domain.ValueObjects;
using MediatR;

namespace Application.User.Authentication
{
    public class AuthenticationCommand : IRequest<AuthenticationResponse>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
