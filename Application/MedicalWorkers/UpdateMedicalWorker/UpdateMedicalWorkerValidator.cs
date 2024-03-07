using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace Application.MedicalWorkers.UpdateMedicalWorker
{
    public class UpdateMedicalWorkerValidator : AbstractValidator<UpdateMedicalWorkerCommand>
    {
        public UpdateMedicalWorkerValidator()
        {
             RuleFor(x => x.City)
                .NotNull()
                .MinimumLength(3)
                .NotEmpty();

            RuleFor(x => x.Street)
                .NotNull()
                .MinimumLength(3)
                .NotEmpty();

            RuleFor(x => x.ZipCode)
                .NotNull()
                .Length(6)
                .NotEmpty();

            RuleFor(x => x.HouseNumber)
                .NotEmpty()
                .NotNull();

            RuleFor(x => x.DateOfBirth)
                .NotEmpty()
                .NotNull();
        }
    }
}