using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace BEQLDT.Data.Infrastructure
{
    public abstract class RepositoryBase<T> : IRepository<T> where T : class
    {
        #region Properties
        private FDbContext dataContext;
        private readonly DbSet<T> dbSet;

        protected IDbFactory DbFactory
        {
            get;
            private set;
        }

        protected FDbContext DbContext
        {
            get { return dataContext ?? (dataContext = DbFactory.GetFDbContext); }
        }
        #endregion

        protected RepositoryBase(IDbFactory dbFactory) : base()
        {
            DbFactory = dbFactory;
            dbSet = DbContext.Set<T>();
        }


        #region Implementation
        public virtual T Add(T entity)
        {
            return dbSet.Add(entity).Entity;
        }

        public virtual void Update(T entity)
        {
            dbSet.Attach(entity);
            dataContext.Entry(entity).State = EntityState.Modified;
        }

        public virtual T Delete(T entity)
        {
            return dbSet.Remove(entity).Entity;
        }
        public virtual T Delete(int id)
        {
            var entity = dbSet.Find(id);
            return dbSet.Remove(entity).Entity;
        }
       

        public virtual T GetSingleById(int id)
        {
            return dbSet.Find(id);
        }

        public virtual IEnumerable<T> GetMany(Expression<Func<T, bool>> where, string includes)
        {
            return dbSet.Where(where).ToList();
        }


        public virtual int Count(Expression<Func<T, bool>> where)
        {
            return dbSet.Count(where);
        }

        public IEnumerable<T> GetAll(string[] includes = null)
        {
            //HANDLE INCLUDES FOR ASSOCIATED OBJECTS IF APPLICABLE
            if (includes != null && includes.Count() > 0)
            {
                var query = dataContext.Set<T>().Include(includes.First());
                foreach (var include in includes.Skip(1))
                    query = query.Include(include);
                return query.AsQueryable();
            }

            return dataContext.Set<T>().AsQueryable();
        }

       
        public T GetSingleByCondition(Expression<Func<T, bool>> expression, string[] includes = null)
        {
            if (includes != null && includes.Count() > 0)
            {
                var query = dataContext.Set<T>().Include(includes.First());
                foreach (var include in includes.Skip(1))
                    query = query.Include(include);
                return query.FirstOrDefault(expression);
            }
            return dataContext.Set<T>().FirstOrDefault(expression);
        }
        public bool CheckContains(Expression<Func<T, bool>> predicate)
        {
            return dataContext.Set<T>().Count<T>(predicate) > 0;
        }

        #endregion
    }
}
