using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using MediatR;

namespace Application.Shifts.GetShiftsByMonthAndYearAndUserId
{
    public class GetShiftsByMonthAndYearAndUserIdQuery : IRequest<IList<Shift>>
    {
        public Guid UserId {get;set;}
        public int Year {get;set;}
        public int Month {get;set;}
    }
}