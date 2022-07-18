using Application.Mapper.Dtos;
using Domain.ValueObjects;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.User.GetUser.GetUserByEmail
{
    public class GetUserByEmailQuery : IRequest<UserDto>
    {
        public Email Email { get; set; }
    }
}
