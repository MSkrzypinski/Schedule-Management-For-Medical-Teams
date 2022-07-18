using Application.Mapper.Dtos;
using Application.Persistence;
using AutoMapper;
using Domain.Entities;
using Domain.ValueObjects;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.MedicalWorkers.CreateNewMedicalWorker
{
    public class CreateNewMedicalWorkerCommandHandler : IRequestHandler<CreateNewMedicalWorkerCommand, CreateNewMedicalWorkerCommandResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMedicalWorkerRepository _medicalWorkerRepository;
        private readonly IGenericCounter<MedicalWorker> _genericCounter;
        private readonly IMapper _mapper;

        public CreateNewMedicalWorkerCommandHandler
            (IUserRepository userRepository, 
            IMedicalWorkerRepository medicalWorkerRepository, 
            IMapper mapper, 
            IGenericCounter<MedicalWorker> genericCounter)
        {
            _userRepository = userRepository;
            _medicalWorkerRepository = medicalWorkerRepository;
            _mapper = mapper;
            _genericCounter = genericCounter;
        }

        public async Task<CreateNewMedicalWorkerCommandResponse> Handle(CreateNewMedicalWorkerCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateNewMedicalWorkerValidator();
            var validatorResult = await validator.ValidateAsync(request);

            if (!validatorResult.IsValid)
            {
                return new CreateNewMedicalWorkerCommandResponse(validatorResult);
            }

            var user = await _userRepository.GetByIdAsync(request.UserId);

            if (user == null)
            {
                return new CreateNewMedicalWorkerCommandResponse("User id is invalid", false);
            }
            var address = _mapper.Map<AddressDto, Address>(request.Address);

            var medicalWorker = MedicalWorker.Create(address, request.DateOfBirth, user, _genericCounter);

            await _medicalWorkerRepository.AddAsync(medicalWorker);

            return new CreateNewMedicalWorkerCommandResponse(medicalWorker.Id);
        }
    }
}
