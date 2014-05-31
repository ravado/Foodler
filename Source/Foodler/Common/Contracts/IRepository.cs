
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Foodler.Common.Contracts
{
    public interface IRepository<T>
    {
        Guid Insert(T model);
        bool Update(T model);
        bool Delete(T model);
        IEnumerable<T> GetAll();
        IEnumerable<T> GetData(Expression<Func<T, bool>> expression);
    }
}
