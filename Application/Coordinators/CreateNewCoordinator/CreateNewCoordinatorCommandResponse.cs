using Application.Responses;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Coordinators.CreateNewCoordinator
{
    public class CreateNewCoordinatorCommandResponse : BaseResponse
    {
        public Guid Id { get; set; }
        public CreateNewCoordinatorCommandResponse()
        {
        }

        public CreateNewCoordinatorCommandResponse(string message = null) : base(message)
        {
        }

        public CreateNewCoordinatorCommandResponse(ValidationResult validationResult) : base(validationResult)
        {
        }

        public CreateNewCoordinatorCommandResponse(string message, bool success) : base(message, success)
        {
        }
        public CreateNewCoordinatorCommandResponse(Guid id)
        {
            Id = id;
            Success = true;
        }
    }
}
