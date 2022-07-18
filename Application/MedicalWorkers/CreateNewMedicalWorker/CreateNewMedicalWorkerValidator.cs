using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.MedicalWorkers.CreateNewMedicalWorker
{
    public class CreateNewMedicalWorkerValidator : AbstractValidator<CreateNewMedicalWorkerCommand>
    {
        public CreateNewMedicalWorkerValidator()
        {
            RuleFor(x => x.Address.City)
                .NotNull()
                .MinimumLength(3)
                .NotEmpty();

            RuleFor(x => x.Address.Street)
                .NotNull()
                .MinimumLength(3)
                .NotEmpty();

            RuleFor(x => x.Address.ZipCode)
                .NotNull()
                .Length(6)
                .NotEmpty();

            RuleFor(x => x.Address.HouseNumber)
                .NotEmpty()
                .NotNull();

            RuleFor(x => x.DateOfBirth)
                .NotEmpty()
                .NotNull();
        }
    }
}
