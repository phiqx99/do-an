using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace BEQLDT.Data.Infrastructure
{
    public interface IRepository<T> where T : class
    {
        T Add(T entity);
        void Update(T entity);
        T Delete(T entity);
        T Delete(int id);
        T GetSingleById(int id);
        T GetSingleByCondition(Expression<Func<T, bool>> expression, string[] includes = null);
        IEnumerable<T> GetAll(string[] includes = null);
        int Count(Expression<Func<T, bool>> where);
        bool CheckContains(Expression<Func<T, bool>> predicate);


    }
}
