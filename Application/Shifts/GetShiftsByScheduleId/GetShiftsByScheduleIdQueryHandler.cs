using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Persistence;
using Domain.Entities;
using MediatR;

namespace Application.Shifts.GetShiftsByScheduleId
{
    public class GetShiftsByScheduleIdQueryHandler : IRequestHandler<GetShiftsByScheduleIdQuery, IList<Shift>>
    {
        private readonly IShiftRepository _shiftRepository;

        public GetShiftsByScheduleIdQueryHandler(IShiftRepository shiftRepository)
        {
            _shiftRepository = shiftRepository;
        }

        public async Task<IList<Shift>> Handle(GetShiftsByScheduleIdQuery request, CancellationToken cancellationToken)
        {
            return await _shiftRepository.GetShiftsByScheduleIdAsync(request.ScheduleId);
        }
    }
}