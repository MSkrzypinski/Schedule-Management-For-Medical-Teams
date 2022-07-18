using Application.Mapper.Dtos;
using Application.Persistence;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Coordinators.GetCoordinator.GetCoordinatorById
{
    public class GetCoordinatorByIdQueryHandler : IRequestHandler<GetCoordinatorByIdQuery,CoordinatorDto>
    {
        private readonly ICoordinatorRepository _coordinatorRepository;
        private readonly IMapper _mapper;

        public GetCoordinatorByIdQueryHandler(ICoordinatorRepository coordinatorRepository, IMapper mapper)
        {
            _coordinatorRepository = coordinatorRepository;
            _mapper = mapper;
        }

        public async Task<CoordinatorDto> Handle(GetCoordinatorByIdQuery request, CancellationToken cancellationToken)
        {
            var coordinator = await _coordinatorRepository.GetCoordinatorIncludeAllPropertiesAsync(request.Id);

            var coordinatorDto = _mapper.Map<Coordinator,CoordinatorDto>(coordinator);

            return coordinatorDto;
        }

     
    }
}
