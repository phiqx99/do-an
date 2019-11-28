using BEQLDT.Data.Infrastructure;
using BEQLDT.Model;
namespace BEQLDT.Data.Repositories
{
    public interface IUserRepository : IRepository<User> { }
     public class UserRepository:RepositoryBase<User> ,IUserRepository
     {
        public UserRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }
     }
}
