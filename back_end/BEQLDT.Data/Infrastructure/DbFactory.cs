using System;
using System.Collections.Generic;
using System.Text;

namespace BEQLDT.Data.Infrastructure
{
    public class DbFactory : Disposable, IDbFactory
    {
        private readonly FDbContext _dbContext;
        public DbFactory(FDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public FDbContext GetFDbContext
        {
            get
            {
                return _dbContext;
            }
        }
        protected override void DisposeCore()
        {
            if (_dbContext != null)
                _dbContext.Dispose();
        }
    }
}
