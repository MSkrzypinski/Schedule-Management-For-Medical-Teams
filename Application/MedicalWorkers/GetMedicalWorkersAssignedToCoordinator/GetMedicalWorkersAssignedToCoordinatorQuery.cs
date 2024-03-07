using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Mapper.Dtos;
using Domain.Entities;
using MediatR;

namespace Application.MedicalWorkers.GetMedicalWorkerAssignedToCoordinator
{
    public class GetMedicalWorkersAssignedToCoordinatorQuery : IRequest<IEnumerable<MedicalWorker>>
    {
        public Guid CoordinatorId { get; set; }
    }
}