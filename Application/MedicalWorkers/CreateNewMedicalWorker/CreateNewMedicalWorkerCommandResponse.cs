using Application.Responses;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.MedicalWorkers.CreateNewMedicalWorker
{
    public class CreateNewMedicalWorkerCommandResponse : BaseResponse
    {
        public Guid Id {get;set;}
        public CreateNewMedicalWorkerCommandResponse()
        {
        }

        public CreateNewMedicalWorkerCommandResponse(string message = null) : base(message)
        {
        }

        public CreateNewMedicalWorkerCommandResponse(ValidationResult validationResult) : base(validationResult)
        {
        }

        public CreateNewMedicalWorkerCommandResponse(string message, bool success) : base(message, success)
        {
        }
        public CreateNewMedicalWorkerCommandResponse(Guid id)
        {
            Id = id;
            Success = true;
            Message = "Medical worker has been created";
        }
    }
}
