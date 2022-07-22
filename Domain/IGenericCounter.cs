using Domain.DDDBlocks;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public interface IGenericCounter<T> where T : Entity
    {
        Task<int> Count(Expression<Func<T,bool>> predicate);
    }
}
