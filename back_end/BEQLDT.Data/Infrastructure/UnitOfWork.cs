using System;
using System.Collections.Generic;
using System.Text;

namespace BEQLDT.Data.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDbFactory dbFactory;
        private FDbContext dbContext;

        public UnitOfWork(IDbFactory dbFactory)
        {
            this.dbFactory = dbFactory;
        }

        public FDbContext DbContext
        {
            get { return dbContext ?? (dbContext = dbFactory.GetFDbContext); }
        }

        public void Commit()
        {
            DbContext.SaveChanges();
        }
    }
}
