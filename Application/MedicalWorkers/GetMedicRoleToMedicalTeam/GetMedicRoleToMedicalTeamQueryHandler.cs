using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Persistence;
using Domain.ValueObjects;
using Domain.ValueObjects.Enums;
using MediatR;

namespace Application.MedicalWorkers.GetMedicRoleToMedicalTeam
{
    public class GetMedicalWorkerProfessionToMedicalTeamQueryHandler : IRequestHandler<GetMedicalWorkerProfessionToMedicalTeamQuery, IList<MedicalWorkerProfessionsToPermissions>>
    {
        private readonly IMedicalTeamRepository _medicalTeamRepository;
        private readonly IBaseRepository<MedicalWorkerProfessionsToPermissions> _BaseRepository;

        public GetMedicalWorkerProfessionToMedicalTeamQueryHandler(IMedicalTeamRepository medicalTeamRepository, 
        IBaseRepository<MedicalWorkerProfessionsToPermissions> baseRepository)
        {
            _medicalTeamRepository = medicalTeamRepository;
            _BaseRepository = baseRepository;
        }

        public async Task<IList<MedicalWorkerProfessionsToPermissions>> Handle(GetMedicalWorkerProfessionToMedicalTeamQuery request, CancellationToken cancellationToken)
        {
            var medicalTeam = await _medicalTeamRepository.GetByIdAsync(request.MedicalTeamId);

            var professionToPermission = await _BaseRepository.GetAll();

            if (medicalTeam == null)
            {
                throw new ArgumentNullException("Medical team not found");
            }

            return professionToPermission
                .Where(x => x.MedicalTeamType == medicalTeam.InformationAboutTeam.MedicalTeamType
                    && x.MedicalWorkerProfession == request.MedicalWorkerProfessionEnum).Distinct().ToList();
            
        }
    }
}