using Application.Persistence;

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly ScheduleManagementContext _scheduleManagementContext;

        public BaseRepository(ScheduleManagementContext scheduleManagementContext)
        {
            _scheduleManagementContext = scheduleManagementContext;
        }

        public async Task<T> AddAsync(T entity)
        {
            await _scheduleManagementContext.Set<T>().AddAsync(entity);
            await _scheduleManagementContext.SaveChangesAsync();

            return entity;
        }

        public async Task DeleteAsync(T entity)
        {
            _scheduleManagementContext.Set<T>().Remove(entity);
            await _scheduleManagementContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> FindBy(Expression<Func<T, bool>> predicate)
        {
            var result =  _scheduleManagementContext.Set<T>().Where(predicate.Compile());
            return await Task.FromResult(result);
        }

        public async Task<IList<T>> GetAll()
        {
            return await _scheduleManagementContext.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            return await _scheduleManagementContext.Set<T>().FindAsync(id);
        }

        public async Task UpdateAsync(T entity)
        {
            _scheduleManagementContext.Entry(entity).State = EntityState.Modified;
            await _scheduleManagementContext.SaveChangesAsync();
        }
    }
}
