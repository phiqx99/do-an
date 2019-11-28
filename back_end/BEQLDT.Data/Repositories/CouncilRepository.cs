using BEQLDT.Data.Infrastructure;
using BEQLDT.Model;
namespace BEQLDT.Data.Repositories
{
    public interface ICouncilRepository : IRepository<Council> { }
    public class CouncilRepository:RepositoryBase<Council>, ICouncilRepository
    {
        public CouncilRepository(IDbFactory dbFactory) : base(dbFactory) { }
    }
}
