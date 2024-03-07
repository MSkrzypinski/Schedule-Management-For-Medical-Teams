using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Mapper.Dtos;
using Application.MedicalWorkers.GetMedicalWorkerAssignedToCoordinator;
using Application.Persistence;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.MedicalWorkers.GetMedicalWorkersAssignedToCoordinator
{
    public class GetMedicalWorkersAssignedToCoordinatorQueryHandler : IRequestHandler<GetMedicalWorkersAssignedToCoordinatorQuery, IEnumerable<MedicalWorker>>
    {
        private readonly IMedicalWorkerRepository _medicalWorkerRepository;
        private readonly IMapper _mapper;

        public GetMedicalWorkersAssignedToCoordinatorQueryHandler(IMedicalWorkerRepository medicalWorkerRepository, IMapper mapper)
        {
            _medicalWorkerRepository = medicalWorkerRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MedicalWorker>> Handle(GetMedicalWorkersAssignedToCoordinatorQuery request, CancellationToken cancellationToken)
        {
           return await _medicalWorkerRepository.GetMedicalWorkersAssignedToCoordinatorByCoordinatorIdAsync(request.CoordinatorId);
        }
    }
}