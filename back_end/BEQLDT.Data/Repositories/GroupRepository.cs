using BEQLDT.Data.Infrastructure;
using BEQLDT.Model;

namespace BEQLDT.Data.Repositories
{
    public interface IGroupRepository : IRepository<Group>
    {

    }
     public class GroupRepository:RepositoryBase<Group>,IGroupRepository
     {
        public GroupRepository(IDbFactory dbFactory) : base(dbFactory) { }
     }
}
