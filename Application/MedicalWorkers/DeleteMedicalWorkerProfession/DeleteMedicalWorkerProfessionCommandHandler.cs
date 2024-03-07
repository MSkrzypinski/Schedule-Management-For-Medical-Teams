using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Persistence;
using AutoMapper;
using Domain.ValueObjects;
using FluentValidation;
using MediatR;

namespace Application.MedicalWorkers.DeleteMedicalWorkerProfession
{
    public class DeleteMedicalWorkerProfessionCommandHandler : IRequestHandler<DeleteMedicalWorkerProfessionCommand, Unit>
    {
        private readonly IMedicalWorkerRepository _medicalWorkerRepository;

        public DeleteMedicalWorkerProfessionCommandHandler(IMedicalWorkerRepository medicalWorkerRepository)
        {
            _medicalWorkerRepository = medicalWorkerRepository;
        }

        public async Task<Unit> Handle(DeleteMedicalWorkerProfessionCommand request, CancellationToken cancellationToken)
        {
            var medicalWorker = await _medicalWorkerRepository.GetByIdAsync(request.medicalWorkerId);
            
            if (medicalWorker == null)
            {
                throw new ApplicationException("Medical worker not found");
            }           

            medicalWorker.MedicalWorkerProfessions = medicalWorker.MedicalWorkerProfessions.Filter(x=>x.MedicalWorkerProfessionEnum != request.medicalWorkerProfessionEnum).ToList();

            await _medicalWorkerRepository.UpdateAsync(medicalWorker);

            return Unit.Value;
        }

       
    }
}