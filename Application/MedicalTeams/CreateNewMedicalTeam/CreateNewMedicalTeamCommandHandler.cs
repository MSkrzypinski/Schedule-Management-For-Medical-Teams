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

namespace Application.MedicalTeams.CreateNewMedicalTeam
{
    public class CreateNewMedicalTeamCommandHandler : IRequestHandler<CreateNewMedicalTeamCommand, CreateNewMedicalTeamCommandResponse>
    {
        private readonly IMedicalTeamRepository _medicalTeamRepository;
        private readonly ICoordinatorRepository _coordinatorRepository;
        private readonly IGenericCounter<MedicalTeam> _genericCounter;

        public CreateNewMedicalTeamCommandHandler
            (IMedicalTeamRepository medicalTeamRepository, 
            ICoordinatorRepository coordinatorRepository, 
            IGenericCounter<MedicalTeam> genericCounter)
        {
            _medicalTeamRepository = medicalTeamRepository;
            _coordinatorRepository = coordinatorRepository;
            _genericCounter = genericCounter;
        }

        public async Task<CreateNewMedicalTeamCommandResponse> Handle(CreateNewMedicalTeamCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateNewMedicalTeamCommandValidator();
            var validatorResult = await validator.ValidateAsync(request);

            if (!validatorResult.IsValid)
            {
                return new CreateNewMedicalTeamCommandResponse(validatorResult);
            }

            var coordinator = await _coordinatorRepository.GetCoordinatorIncludeAllPropertiesAsync(request.CoordinatorId);

            if (coordinator == null)
            {
                return new CreateNewMedicalTeamCommandResponse("Coordinator is invalid", false);
            }

            var informationAboutTeam = new InformationAboutTeam(request.Code,request.City,request.SizeOfTeam,request.MedicalTeamType);

            var medicalTeam = MedicalTeam.Create(informationAboutTeam, coordinator, _genericCounter);

            await _medicalTeamRepository.AddAsync(medicalTeam);

            return new CreateNewMedicalTeamCommandResponse(medicalTeam.Id);
        }
    }
}
