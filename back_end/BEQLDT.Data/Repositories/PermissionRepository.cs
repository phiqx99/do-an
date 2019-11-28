using BEQLDT.Data.Infrastructure;
using BEQLDT.Model;

namespace BEQLDT.Data.Repositories
{
    public interface IPermissionRepository : IRepository<Permission> { }
    public class PermissionRepository:RepositoryBase<Permission>,IPermissionRepository
    {
        public PermissionRepository(IDbFactory dbFactory) : base(dbFactory) { }
    }
}
