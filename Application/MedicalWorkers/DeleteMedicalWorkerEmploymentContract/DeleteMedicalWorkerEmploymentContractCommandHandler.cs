using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Persistence;
using MediatR;

namespace Application.MedicalWorkers.DeleteMedicalWorkerEmploymentContract
{
    public class DeleteMedicalWorkerEmploymentContractCommandHandler : IRequestHandler<DeleteMedicalWorkerEmploymentContractCommand, Unit>
    {
        private readonly IMedicalWorkerRepository _medicalWorkerRepository;

        public DeleteMedicalWorkerEmploymentContractCommandHandler(IMedicalWorkerRepository medicalWorkerRepository)
        {
            _medicalWorkerRepository = medicalWorkerRepository;
        }

        public async Task<Unit> Handle(DeleteMedicalWorkerEmploymentContractCommand request, CancellationToken cancellationToken)
        {
            var medicalWorker = await _medicalWorkerRepository.GetMedicalWorkerByIdIncludeAllPropertiesAsync(request.MedicalWorkerId);
            
            if (medicalWorker == null)
            {
                throw new ApplicationException("Medical worker not found");
            }           

            medicalWorker.EmploymentContracts = medicalWorker.EmploymentContracts
                .Where(x=>x.ContractType.ToString() != request.ContractType
                        || x.MedicalWorkerProfession.ToString() != request.MedicalWorkerProfession
                        || x.MedicRole.ToString() != request.MedicRole
                        || x.MedicalTeam.Id != request.MedicalTeamId).ToList();

            await _medicalWorkerRepository.UpdateAsync(medicalWorker);

            return Unit.Value;
        }
    }
}