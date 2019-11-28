using BEQLDT.Data.Infrastructure;
using BEQLDT.Model;
namespace BEQLDT.Data.Repositories
{
    public interface ITopicRepository : IRepository<Topic> { }
    public class TopicRepository:RepositoryBase<Topic>, ITopicRepository
    {
        public TopicRepository(IDbFactory dbFactory) : base(dbFactory) { }
    }
}
