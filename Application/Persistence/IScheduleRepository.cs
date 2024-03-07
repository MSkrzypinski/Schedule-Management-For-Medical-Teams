using Domain.Entities;

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Persistence
{
    public interface IScheduleRepository : IBaseRepository<Schedule>
    {
      public Task<Schedule> GetScheduleByMonthAndYearAndMedicalTeamIdIncludeAllPropertiesAsync(Guid medicalTeamId,int month,int year);
    }
}
