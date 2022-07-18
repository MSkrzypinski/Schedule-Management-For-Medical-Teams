using Application.Responses;
using FluentValidation.Results;

namespace Application.Shifts.AddDriver
{
    public class AddMedicalWorkerCommandResponse : BaseResponse
    {
        public AddMedicalWorkerCommandResponse()
        {
        }

        public AddMedicalWorkerCommandResponse(string message = null) : base(message)
        {
        }

        public AddMedicalWorkerCommandResponse(ValidationResult validationResult) : base(validationResult)
        {
        }

        public AddMedicalWorkerCommandResponse(string message, bool success) : base(message, success)
        {
        }
    }
}