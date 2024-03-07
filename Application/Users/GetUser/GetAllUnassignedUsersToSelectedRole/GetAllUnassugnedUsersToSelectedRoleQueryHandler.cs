using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Mapper.Dtos;
using Application.Persistence;
using Application.Users.GetUser.GetAllUnassignedUsersToMedicalWorker;
using AutoMapper;
using MediatR;

namespace Application.Users.GetUser.GetAllUnassignedUsersToSelectedRole
{
    public class GetAllUnassugnedUsersToSelectedRoleQueryHandler: IRequestHandler<GetAllUnassugnedUsersToSelectedRoleQuery, IEnumerable<UserDto>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GetAllUnassugnedUsersToSelectedRoleQueryHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserDto>> Handle(GetAllUnassugnedUsersToSelectedRoleQuery request, CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetAllUnassignedUsersToSelectedRole(request.Role);

            return _mapper.Map<IEnumerable<Domain.Entities.User>,IEnumerable<UserDto>>(users);        
        }
    }

}