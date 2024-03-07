using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace Application.MedicalTeams.UpdateMedicalTeam
{
    public class UpdateMedicalTeamValidator : AbstractValidator<UpdateMedicalTeamCommand>
    {
        public UpdateMedicalTeamValidator()
        {
             RuleFor(x => x.City)
                .NotNull()
                .NotEmpty();

            RuleFor(x => x.SizeOfTeam)
                .GreaterThanOrEqualTo(1);
        }
    }
}
