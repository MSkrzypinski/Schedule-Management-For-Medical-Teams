using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using MediatR;

namespace Application.MedicalWorkers.GetMedicalWorkerByUserId
{
    public class GetMedicalWorkerByUserIdQuery : IRequest<MedicalWorker>
    {
        public Guid UserId {get;set;}
    }
}