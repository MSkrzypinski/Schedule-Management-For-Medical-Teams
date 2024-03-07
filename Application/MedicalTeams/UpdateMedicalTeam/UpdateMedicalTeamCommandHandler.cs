 using Application.Mapper.Dtos;
using Application.Persistence;
using AutoMapper;
using Domain.Entities;
using Domain.ValueObjects;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.MedicalTeams.UpdateMedicalTeam
{

    public class UpdateMedicalTeamCommandHandler : IRequestHandler<UpdateMedicalTeamCommand, Guid>
    {
        private readonly IMedicalTeamRepository _medicalTeamRepository;
        private readonly ICoordinatorRepository _coordinatorRepository;

        public UpdateMedicalTeamCommandHandler
            (IMedicalTeamRepository medicalTeamRepository, 
            ICoordinatorRepository coordinatorRepository)
        {
            _medicalTeamRepository = medicalTeamRepository;
            _coordinatorRepository = coordinatorRepository;
        }

        public async Task<Guid> Handle(UpdateMedicalTeamCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateMedicalTeamValidator();
            var validatorResult = await validator.ValidateAsync(request);

            if (!validatorResult.IsValid)
            {
                throw new ValidationException("Validation failed");
            }
            var medicalTeam = await _medicalTeamRepository.GetByIdAsync(request.Id);
            if (medicalTeam == null)
            {
                throw new ApplicationException("Medical team not found");
            }
            var coordinator = await _coordinatorRepository.GetByIdAsync(request.CoordinatorId);

            var informationAboutTeam = new InformationAboutTeam(medicalTeam.InformationAboutTeam.Code, request.City, request.SizeOfTeam, request.MedicalTeamType);

            medicalTeam.InformationAboutTeam = informationAboutTeam;
            if (coordinator != null)
            {
                medicalTeam.Coordinator = coordinator;
            }

            await _medicalTeamRepository.UpdateAsync(medicalTeam);

            return medicalTeam.Id;
        }
    }
}

