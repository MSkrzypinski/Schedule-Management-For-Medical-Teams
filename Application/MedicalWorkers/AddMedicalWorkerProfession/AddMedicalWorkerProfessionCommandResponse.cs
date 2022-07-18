using Application.Mapper.Dtos;
using Application.Responses;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.MedicalWorkers.AddMedicalWorkerProfession
{
    public class AddMedicalWorkerProfessionCommandResponse : BaseResponse
    {
        public AddMedicalWorkerProfessionCommandResponse()
        {
        }

        public AddMedicalWorkerProfessionCommandResponse(string message = null) : base(message)
        {
        }

        public AddMedicalWorkerProfessionCommandResponse(ValidationResult validationResult) : base(validationResult)
        {
        }

        public AddMedicalWorkerProfessionCommandResponse(string message, bool success) : base(message, success)
        {
        }
       
    }
}
