using Application.Authorization;
using Application.Mapper.Dtos;
using Application.Persistence;
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
    public class AddDayOffCommandHandler : IRequestHandler<AddDayOffCommand, Unit>
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

        public async Task<Unit> Handle(AddDayOffCommand request, CancellationToken cancellationToken)
        {
            var medicalWorker = await _medicalWorkerRepository.GetMedicalWorkerByIdIncludeAllPropertiesAsync(request.MedicalWorkerId);

            if (medicalWorker == null)
            {
                throw new ArgumentNullException("Invalid medical worker");
            }

            /*var authorizationResult = _authorizationService.AuthorizeAsync
                (_userExecusionContextAccessor.User, medicalWorker, new UserMustBeThisMedicalWorkerRequirement()).Result;

            if (!authorizationResult.Succeeded)
            {
                throw new UnauthorizedAccessException("Authorization failed");
            }*/

            medicalWorker.AddDayOff(request.Start, request.End);

            await _medicalWorkerRepository.UpdateAsync(medicalWorker);

            return Unit.Value;
        }
    }
}
