using Application.Persistence;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Application.Authorization;

namespace Application.MedicalWorkers.CreateNewEmploymentContract
{
    public class CreateNewEmploymentContractCommandHandler : IRequestHandler<CreateNewEmploymentContractCommand, CreateNewEmploymentContractCommandResponse>
    {
        private readonly IMedicalWorkerRepository _medicalWorkerRepository;
        private readonly IMedicalTeamRepository _medicalTeamRepository;
        private readonly IAuthorizationService _authorizationService;
        private readonly IUserExecusionContextAccessor _userExecusionContextAccessor;


        public CreateNewEmploymentContractCommandHandler
            (IMedicalWorkerRepository medicalWorkerRepository, 
            IMedicalTeamRepository medicalTeamRepository, 
            IAuthorizationService authorizationService, 
            IUserExecusionContextAccessor userExecusionContextAccessor)
        {
            _medicalWorkerRepository = medicalWorkerRepository;
            _medicalTeamRepository = medicalTeamRepository;
            _authorizationService = authorizationService;
            _userExecusionContextAccessor = userExecusionContextAccessor;
        }

        public async Task<CreateNewEmploymentContractCommandResponse> Handle(CreateNewEmploymentContractCommand request, CancellationToken cancellationToken)
        {
            var medicalWorker = await _medicalWorkerRepository.GetByIdAsync(request.MedicalWorkerId);

            if (medicalWorker == null)
            {
                return new CreateNewEmploymentContractCommandResponse("User id is invalid", false);
            }

            if (!medicalWorker.MedicalWorkerProfessions.Any(x => x.MedicalWorkerProfessionEnum.Equals(request.MedicalWorkerProfession)))
            {
                return new CreateNewEmploymentContractCommandResponse("User doesn't have the requirement profession", false);
            }

            var medicalTeam = await _medicalTeamRepository.GetByIdAsync(request.MedicalTeamId);

            var authorizationResult = _authorizationService.AuthorizeAsync
                (_userExecusionContextAccessor.User, medicalTeam, new MustBeCoordinatorForThisTeamRequirement()).Result;

            if (!authorizationResult.Succeeded)
            {
                return new CreateNewEmploymentContractCommandResponse("Authorization failed", false);
            }

            var permissions = await _medicalWorkerRepository.GetPermissionToProfessionAsync(request.MedicalWorkerProfession);

            var permissionResult = permissions
                .Any(x =>
                        x.MedicalWorkerProfession.Equals(request.MedicalWorkerProfession)&& 
                        x.MedicRole.Equals(request.MedicRole)&& 
                        x.MedicalTeamType.Equals(medicalTeam.InformationAboutTeam.MedicalTeamType)
                    );

            if (!permissionResult)
            {
                return new CreateNewEmploymentContractCommandResponse("Medical worker doesn't have the requirement permission", false);
            }

            medicalWorker.AddEmploymentContract(medicalTeam, request.ContractType, request.MedicRole, request.MedicalWorkerProfession);
            
            await _medicalWorkerRepository.UpdateAsync(medicalWorker);

            return new CreateNewEmploymentContractCommandResponse("Employment contract has been added", true);
        }
    }
}
