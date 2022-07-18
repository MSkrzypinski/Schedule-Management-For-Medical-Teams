using Domain.ValueObjects.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Shifts.AddDriver
{
    public class AddMedicalWorkerCommand : IRequest<AddMedicalWorkerCommandResponse>
    {
        public MedicRole MedicRole { get; set; }
        public Guid MedicalWorkerId { get; set; }
        public Guid ShiftId { get; set; }
    }
}
