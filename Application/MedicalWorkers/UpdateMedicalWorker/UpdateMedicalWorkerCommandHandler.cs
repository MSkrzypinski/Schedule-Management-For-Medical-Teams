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

namespace Application.MedicalWorkers.UpdateMedicalWorker
{
    public class UpdateMedicalWorkerCommandHandler : IRequestHandler<UpdateMedicalWorkerCommand, Unit>
    {
        private readonly IMedicalWorkerRepository _medicalWorkerRepository;
        private readonly IMapper _mapper;

        public UpdateMedicalWorkerCommandHandler
            (IMedicalWorkerRepository medicalWorkerRepository, 
            IMapper mapper
            )
        {
            _medicalWorkerRepository = medicalWorkerRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateMedicalWorkerCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateMedicalWorkerValidator();
            var validatorResult = await validator.ValidateAsync(request);

            if (!validatorResult.IsValid)
            {
                throw new ValidationException("Validation failed");
            }
            var medicalWorker = await _medicalWorkerRepository.GetByIdAsync(request.Id);
            
            if (medicalWorker == null)
            {
                throw new ApplicationException("Medical worker not found");
            }           

            var address = new Address(request.City,request.ZipCode,request.Street,request.HouseNumber,request.ApartmentNumber);

            medicalWorker.Address = address;

            await _medicalWorkerRepository.UpdateAsync(medicalWorker);

            return Unit.Value;
        }
    }
}