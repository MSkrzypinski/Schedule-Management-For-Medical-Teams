using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Persistence;
using Domain.ValueObjects;
using MediatR;

namespace Application
{
    public class GetProfessionToPermissionCommand : IRequest<IList<MedicalWorkerProfessionsToPermissions>>
    {
      
    }
    public class GetProfessionToPermissionCommandHandler : IRequestHandler<GetProfessionToPermissionCommand,IList<MedicalWorkerProfessionsToPermissions>>
    {
        private readonly IBaseRepository<MedicalWorkerProfessionsToPermissions> _BaseRepository;
        public GetProfessionToPermissionCommandHandler(IBaseRepository<MedicalWorkerProfessionsToPermissions> baseRepository)
        {
            _BaseRepository = baseRepository;
        }

        public async Task<IList<MedicalWorkerProfessionsToPermissions>> Handle(GetProfessionToPermissionCommand request, CancellationToken cancellationToken)
        {
            return await _BaseRepository.GetAll();
        }
    }
}