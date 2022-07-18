using Application.Mapper.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.MedicalWorkers.CreateNewMedicalWorker
{
    public class CreateNewMedicalWorkerCommand : IRequest<CreateNewMedicalWorkerCommandResponse>
    {
        public AddressDto Address {get;set;}
        public DateTime DateOfBirth { get; set; }
        public Guid UserId { get; set; }

    }
}
