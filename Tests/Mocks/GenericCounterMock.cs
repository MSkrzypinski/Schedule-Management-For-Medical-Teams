using Domain.DDDBlocks;
using Domain.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Mock
{
    public class GenericCounterMock<T> where T : Entity
    {
        public static Mock<IGenericCounter<T>> Mock(List<T> list)
        {
            var repo = new Mock<IGenericCounter<T>>();
            repo.Setup(repo => repo.Count(It.IsAny<Expression<Func<T, bool>>>())).ReturnsAsync((Expression<Func<T, bool>> predicate) =>
            {
                int result = list.Where(predicate.Compile()).Count();

                return result;
            });

            return repo;
        }
    }
}
