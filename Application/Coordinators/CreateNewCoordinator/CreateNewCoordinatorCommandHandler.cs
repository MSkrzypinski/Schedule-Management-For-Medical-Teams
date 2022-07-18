using Application.Persistence;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Coordinators.CreateNewCoordinator
{
    public class CreateNewCoordinatorCommandHandler : IRequestHandler<CreateNewCoordinatorCommand, CreateNewCoordinatorCommandResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly ICoordinatorRepository _coordinatorRepository;
        private readonly IGenericCounter<Coordinator> _genericCounter;

        public CreateNewCoordinatorCommandHandler
            (IUserRepository userRepository, 
            ICoordinatorRepository coordinatorRepository, 
            IGenericCounter<Coordinator> genericCounter)
        {
            _userRepository = userRepository;
            _coordinatorRepository = coordinatorRepository;
            _genericCounter = genericCounter;
        }

        public async Task<CreateNewCoordinatorCommandResponse> Handle(CreateNewCoordinatorCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.UserId);

            if (user == null)
            {
                return new CreateNewCoordinatorCommandResponse("Invalid Id",false);
            }

            var coordinator = Coordinator.Create(user, _genericCounter);

            await _coordinatorRepository.AddAsync(coordinator);

            return new CreateNewCoordinatorCommandResponse(coordinator.Id);

        }
    }
}
