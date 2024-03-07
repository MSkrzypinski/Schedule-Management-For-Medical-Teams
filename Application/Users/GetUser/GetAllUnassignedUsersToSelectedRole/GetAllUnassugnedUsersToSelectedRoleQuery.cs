using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Mapper.Dtos;
using MediatR;

namespace Application.Users.GetUser.GetAllUnassignedUsersToMedicalWorker
{
    public class GetAllUnassugnedUsersToSelectedRoleQuery : IRequest<IEnumerable<UserDto>> 
    {
        public string Role { get; set; }
    }
}