using Domain.DDDBlocks;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class GenericCounter<T> : IGenericCounter<T> where T : Entity
    {
        protected readonly ScheduleManagementContext _scheduleManagementContext;

        public GenericCounter(ScheduleManagementContext scheduleManagementContext)
        {
            _scheduleManagementContext = scheduleManagementContext;
        }

        public async Task<int> Count(Expression<Func<T, bool>> predicate)
        {
            if (typeof(T) == typeof(Schedule))
                return await Task.FromResult(_scheduleManagementContext.Set<T>().Include("MedicalTeam").Where(predicate.Compile()).Count());

            if (typeof(T) == typeof(MedicalWorker) || typeof(T) == typeof(Coordinator))
                return await Task.FromResult(_scheduleManagementContext.Set<T>().Include("User").Where(predicate.Compile()).Count());

            var result = _scheduleManagementContext.Set<T>().Where(predicate.Compile()).Count();

            return await Task.FromResult(result);
        }
    }
}
