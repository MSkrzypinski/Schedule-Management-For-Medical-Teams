 using Application.Mapper.Dtos;
using Application.Persistence;
using Application.User.GetUser.GetUserByEmail;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Coordinators.GetAllMedicalTeamsAssignedToCoordinator
{
    public class GetAllMedicalTeamsAssignedToCoordinatorQueryHandler : IRequestHandler<GetAllMedicalTeamsAssignedToCoordinatorQuery, IEnumerable<MedicalTeamDto>>
    {
        private readonly ICoordinatorRepository _coordinatorRepository;
        private readonly IMapper _mapper;

        public GetAllMedicalTeamsAssignedToCoordinatorQueryHandler(ICoordinatorRepository coordinatorRepository, IMapper mapper)
        {
            _coordinatorRepository = coordinatorRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MedicalTeamDto>> Handle(GetAllMedicalTeamsAssignedToCoordinatorQuery request, CancellationToken cancellationToken)
        {
            var medicalTeams = await _coordinatorRepository.GetAllMedicalTeamsAssignedToCoordinatorByUserId(request.UserId);

            return _mapper.Map<IEnumerable<Domain.Entities.MedicalTeam>,IEnumerable<MedicalTeamDto>>(medicalTeams);
        }

    }
}

    
