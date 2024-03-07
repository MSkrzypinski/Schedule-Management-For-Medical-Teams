using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using MediatR;

namespace Application.Shifts.GetShiftsByScheduleId
{
    public class GetShiftsByScheduleIdQuery : IRequest<IList<Shift>>
    {
        public Guid ScheduleId {get;set;}
    }
}