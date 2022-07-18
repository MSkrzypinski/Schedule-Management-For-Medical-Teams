using Application.Mapper.Dtos;

using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.User.GetUserById.GetUser
{
    public class GetUserByIdQuery : IRequest<UserDto>
    {
        public Guid Id { get; set; }
    }
}
