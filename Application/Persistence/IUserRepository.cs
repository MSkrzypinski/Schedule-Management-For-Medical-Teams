﻿using Domain.ValueObjects;
using System.Threading.Tasks;

namespace Application.Persistence
{
    public interface IUserRepository : IBaseRepository<Domain.Entities.User>
    {
        Task<Domain.Entities.User> FindByEmailAsync(Email email);
    }
}