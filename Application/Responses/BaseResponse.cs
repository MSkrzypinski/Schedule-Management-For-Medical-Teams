using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation.Results;

namespace Application.Responses
{
    public abstract class BaseResponse
    {
        public ResponseStatus Status { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
        public List<string> ValidationErrors { get; set; }

        public BaseResponse()
        {
            ValidationErrors = new List<string>();
            Success = true;
        }
        public BaseResponse(string message = null)
        {
            ValidationErrors = new List<string>();
            Success = true;
            Message = message;
        }

        public BaseResponse(string message, bool success)
        {
            ValidationErrors = new List<string>();
            Success = success;
            Message = message;
        }

        public BaseResponse(ValidationResult validationResult)
        {
            Status = ResponseStatus.Success;

            ValidationErrors = new List<string>();

            Success = validationResult.Errors.Count < 1;

            foreach (var item in validationResult.Errors)
            {
                ValidationErrors.Add(item.ErrorMessage);
            }

            if (!Success)
                Status = ResponseStatus.ValidationError;
        }


    }

}
