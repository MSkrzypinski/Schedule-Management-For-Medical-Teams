using Application.Responses;
using FluentValidation.Results;
using System;

namespace Application.Schedules.CreateNewSchedule
{
    public class CreateNewScheduleCommandResponse : BaseResponse
    {
        public Guid Id { get; set; }
        public CreateNewScheduleCommandResponse()
        {
        }

        public CreateNewScheduleCommandResponse(string message = null) : base(message)
        {
        }

        public CreateNewScheduleCommandResponse(ValidationResult validationResult) : base(validationResult)
        {
        }

        public CreateNewScheduleCommandResponse(string message, bool success) : base(message, success)
        {
        }

        public CreateNewScheduleCommandResponse(Guid id,string message,bool success)
        {
            Id = id;
            Message = message;
            Success = success;
        }
    }
}