using BEQLDT.Data.Infrastructure;
using BEQLDT.Model;

namespace BEQLDT.Data.Repositories
{
     public interface IRoleRepository : IRepository<Role> { }
    public class RoleRepository:RepositoryBase<Role>,IRoleRepository
    {
        public RoleRepository(IDbFactory dbFactory) : base(dbFactory) { }
    }
}
