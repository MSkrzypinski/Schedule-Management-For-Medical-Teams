using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Application.Persistence;
using Domain.Entities;
using MediatR;

namespace Application.MedicalWorkers.GetMedicalWorkerByUserId
{
    public class GetMedicalWorkerByUserIdQueryHandler : IRequestHandler<GetMedicalWorkerByUserIdQuery, MedicalWorker>
    {
        private readonly IMedicalWorkerRepository _medicalWorkerRepository;

        public GetMedicalWorkerByUserIdQueryHandler(IMedicalWorkerRepository medicalWorkerRepository)
        {
            _medicalWorkerRepository = medicalWorkerRepository;
        }

        public async Task<MedicalWorker> Handle(GetMedicalWorkerByUserIdQuery request, CancellationToken cancellationToken)
        {
            var medicalWorker = await _medicalWorkerRepository.GetMedicalWorkerByUserIdIncludeAllPropertiesAsync(request.UserId);

            if (medicalWorker == null)
            {
                throw new ArgumentNullException("Medical worker not found.");
            }
            return medicalWorker;
        }
    }
}