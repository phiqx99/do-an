using BEQLDT.Data.Infrastructure;
using BEQLDT.Model;

namespace BEQLDT.Data.Repositories
{
    public interface IGroupUserRepository : IRepository<GroupUser> { }
    public class GroupUserRepository:RepositoryBase<GroupUser>,IGroupUserRepository
    {
        public GroupUserRepository(IDbFactory dbFactory) : base(dbFactory) { }
    }
}
