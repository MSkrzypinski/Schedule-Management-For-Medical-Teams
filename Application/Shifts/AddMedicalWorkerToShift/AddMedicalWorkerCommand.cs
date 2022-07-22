using Domain.ValueObjects.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Shifts.AddMedicalWorkerToShift
{
    public class AddMedicalWorkerCommand : IRequest<Unit>
    {
        public MedicRole MedicRole { get; set; }
        public Guid MedicalWorkerId { get; set; }
        public Guid ShiftId { get; set; }
    }
}
