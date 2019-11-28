using BEQLDT.Data.Infrastructure;
using BEQLDT.Model;

namespace BEQLDT.Data.Repositories
{
    public interface IDecisionRepository : IRepository<Decision> { }
    public class DecisionRepository:RepositoryBase<Decision>, IDecisionRepository
    {
        public DecisionRepository(IDbFactory dbFactory) : base(dbFactory) { }
    }
}
