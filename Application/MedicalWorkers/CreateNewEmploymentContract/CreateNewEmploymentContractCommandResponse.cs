using Application.Responses;
using FluentValidation.Results;

namespace Application.MedicalWorkers.CreateNewEmploymentContract
{
    public class CreateNewEmploymentContractCommandResponse : BaseResponse
    {
        public CreateNewEmploymentContractCommandResponse()
        {
        }

        public CreateNewEmploymentContractCommandResponse(string message = null) : base(message)
        {
        }

        public CreateNewEmploymentContractCommandResponse(ValidationResult validationResult) : base(validationResult)
        {
        }

        public CreateNewEmploymentContractCommandResponse(string message, bool success) : base(message, success)
        {
        }
    }
}