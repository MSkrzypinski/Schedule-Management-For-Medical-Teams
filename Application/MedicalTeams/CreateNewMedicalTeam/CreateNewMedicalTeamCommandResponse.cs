using Application.Responses;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.MedicalTeams.CreateNewMedicalTeam
{
    public class CreateNewMedicalTeamCommandResponse : BaseResponse
    {
        public Guid Id { get; set; }
        public CreateNewMedicalTeamCommandResponse()
        {
        }

        public CreateNewMedicalTeamCommandResponse(string message = null) : base(message)
        {
        }

        public CreateNewMedicalTeamCommandResponse(ValidationResult validationResult) : base(validationResult)
        {
        }

        public CreateNewMedicalTeamCommandResponse(string message, bool success) : base(message, success)
        {
        }

        public CreateNewMedicalTeamCommandResponse(Guid id)
        {
            Id = id;
            Success = true;
            Message = "Medical team has been created";
        }
    }
}
