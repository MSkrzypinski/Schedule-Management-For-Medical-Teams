using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Persistence;
using Domain.Entities;
using MediatR;

namespace Application.Shifts.GetShiftsByMonthAndYearAndUserId
{
    public class GetShiftsByMonthAndYearAndUserIdQueryHandler : IRequestHandler<GetShiftsByMonthAndYearAndUserIdQuery, IList<Shift>>
    {
        private readonly IShiftRepository _shiftRepository;

        public GetShiftsByMonthAndYearAndUserIdQueryHandler(IShiftRepository shiftRepository)
        {
            _shiftRepository = shiftRepository;
        }

        public async Task<IList<Shift>> Handle(GetShiftsByMonthAndYearAndUserIdQuery request, CancellationToken cancellationToken)
        {
            return await _shiftRepository.GetShiftsByMonthAndYearAndUserIdIncludeAllPropertiesAsync(request.UserId,request.Month,request.Year);
        }
    }
}