using BEQLDT.Data.Infrastructure;
using BEQLDT.Model;

namespace BEQLDT.Data.Repositories
{
    public interface ITopicCouncilRepository : IRepository<TopicCouncil> { }
    public class TopicCouncilRepository:RepositoryBase<TopicCouncil>,ITopicCouncilRepository
    {
        public TopicCouncilRepository(IDbFactory dbFactory) : base(dbFactory) { }
    }
}
