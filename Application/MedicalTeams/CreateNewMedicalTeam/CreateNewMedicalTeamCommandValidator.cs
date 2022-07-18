using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.MedicalTeams.CreateNewMedicalTeam
{
    public class CreateNewMedicalTeamCommandValidator : AbstractValidator<CreateNewMedicalTeamCommand>
    {
        public CreateNewMedicalTeamCommandValidator()
        {
            RuleFor(x => x.City)
                .NotNull()
                .NotEmpty();

            RuleFor(x => x.Code)
                .NotNull()
                .NotEmpty();

            RuleFor(x => x.SizeOfTeam)
                .GreaterThan(1);
        }
    }
}
