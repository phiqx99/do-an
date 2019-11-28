using System;
using System.Collections.Generic;
using System.Text;

namespace BEQLDT.Data.Infrastructure
{
    public interface IDbFactory : IDisposable
    {
        FDbContext GetFDbContext { get; }
    }
}
