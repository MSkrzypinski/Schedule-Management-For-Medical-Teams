using Application.Authorization;
using Application.Mapper.Dtos;
using Application.Persistence;
using AutoMapper;
using Domain.ValueObjects;
using Domain.ValueObjects.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.MedicalWorkers.AddMedicalWorkerProfession
{
    public class AddMedicalWorkerProfessionCommandHandler : IRequestHandler<AddMedicalWorkerProfessionCommand, Unit>
    {
        private readonly IMedicalWorkerRepository _medicalWorkerRepository;

        public AddMedicalWorkerProfessionCommandHandler(IMedicalWorkerRepository medicalWorkerRepository)
        {
            _medicalWorkerRepository = medicalWorkerRepository;
        }

        public async Task<Unit> Handle(AddMedicalWorkerProfessionCommand request, CancellationToken cancellationToken)
        {
            var medicalWorker = await _medicalWorkerRepository.GetMedicalWorkerByIdIncludeAllPropertiesAsync(request.MedicalWorkerId);

            if (medicalWorker == null)
            {
                throw new ArgumentNullException("Invalid medical worker");
            }

            medicalWorker.AddMedicalWorkerProfession(request.MedicalWorkerProfessionEnum);

            await _medicalWorkerRepository.UpdateAsync(medicalWorker);

            return Unit.Value;
        }
    }
}
