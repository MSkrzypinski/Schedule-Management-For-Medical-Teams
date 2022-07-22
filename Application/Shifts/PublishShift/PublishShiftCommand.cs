using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Shifts.PublishShift
{
    public class PublishShiftCommand : IRequest<Unit>
    {
       public Guid ShiftId { get; set; }
    }
}
