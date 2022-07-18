using Application.Persistence;
using Domain.Entities;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Users
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
       
        public UserRepository(ScheduleManagementContext scheduleManagementContext) : base(scheduleManagementContext)
        {
            
        }

        public async Task<User> FindByEmailAsync(Email email)
        {
            var result = _scheduleManagementContext.Users.Where(x => x.Email.Equals(email)).FirstOrDefault();

            return await Task.FromResult(result);
        }

      
    }
}
