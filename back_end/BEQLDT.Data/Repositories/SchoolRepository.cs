using BEQLDT.Data.Infrastructure;
using BEQLDT.Model;

namespace BEQLDT.Data.Repositories
{
    public interface ISchoolRepository : IRepository<School> { }
    public class SchoolRepository: RepositoryBase<School>,ISchoolRepository
    {
        public SchoolRepository(IDbFactory dbFactory) : base(dbFactory) { }
    }
}
