using Application.Mapper.Dtos;
using Domain.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Persistence
{
    public interface IShiftRepository : IBaseRepository<Shift>
    {
        Task<Shift> GetShiftByIdIncludeAllPropertiesAsync(Guid id);
        Task<IList<Shift>> GetShiftsByScheduleIdAsync(Guid scheduleId);
        Task<IList<Shift>> GetShiftsByMonthAndYearAndUserIdIncludeAllPropertiesAsync(Guid userId,int month,int year);
    }
}
