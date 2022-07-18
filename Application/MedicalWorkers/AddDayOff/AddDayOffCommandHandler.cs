using Application.Authorization;
using Application.Mapper.Dtos;
using Application.Persistence;
using Application.Responses;
using AutoMapper;
using Domain.ValueObjects;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.MedicalWorkers.AddDayOff
{
    public class AddDayOffCommandHandler : IRequestHandler<AddDayOffCommand, AddDayOffCommandResponse>
    {
        private readonly IMedicalWorkerRepository _medicalWorkerRepository;
        private readonly IAuthorizationService _authorizationService;
        private readonly IUserExecusionContextAccessor _userExecusionContextAccessor;

        public AddDayOffCommandHandler
            (IMedicalWorkerRepository medicalWorkerRepository, 
            IAuthorizationService authorizationService, 
            IUserExecusionContextAccessor userExecusionContextAccessor)
        {
            _medicalWorkerRepository = medicalWorkerRepository;
            _authorizationService = authorizationService;
            _userExecusionContextAccessor = userExecusionContextAccessor;
        }

        public async Task<AddDayOffCommandResponse> Handle(AddDayOffCommand request, CancellationToken cancellationToken)
        {
            var medicalWorker = await _medicalWorkerRepository.GetMedicalWorkerByIdIncludeAllPropertiesAsync(request.MedicalWorkerId);

            if (medicalWorker == null)
            {
                return new AddDayOffCommandResponse("Medical worker is invalid",false);
            }
            var authorizationResult = _authorizationService.AuthorizeAsync
                (_userExecusionContextAccessor.User, medicalWorker, new UserMustBeThisMedicalWorkerRequirement()).Result;

            if (!authorizationResult.Succeeded)
            {
                return new AddDayOffCommandResponse("Authorization failed", false);
            }

            medicalWorker.AddDayOff(request.Start, request.End);

            await _medicalWorkerRepository.UpdateAsync(medicalWorker);

            return new AddDayOffCommandResponse("Day Off has been added");
        }
    }
}
