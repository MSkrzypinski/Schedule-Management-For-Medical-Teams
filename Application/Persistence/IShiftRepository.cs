using Domain.Entities;

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Persistence
{
    public interface IShiftRepository : IBaseRepository<Shift>
    {
        Task<Shift> GetShiftByIdIncludeAllPropertiesAsync(Guid id);
    }
}
