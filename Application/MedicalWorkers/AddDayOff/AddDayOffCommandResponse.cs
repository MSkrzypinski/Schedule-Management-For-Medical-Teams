using Application.Responses;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.MedicalWorkers.AddDayOff
{
    public class AddDayOffCommandResponse : BaseResponse
    {
        public AddDayOffCommandResponse()
        {
        }

        public AddDayOffCommandResponse(string message = null) : base(message)
        {
        }

        public AddDayOffCommandResponse(ValidationResult validationResult) : base(validationResult)
        {
        }

        public AddDayOffCommandResponse(string message, bool success) : base(message, success)
        {
        }
    }
}
