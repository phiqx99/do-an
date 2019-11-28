using BEQLDT.Data.Infrastructure;
using BEQLDT.Model;

namespace BEQLDT.Data.Repositories
{
    public interface IFileRepository : IRepository<Filed> { }
    public class FileRepository:RepositoryBase<Filed>,IFileRepository
    {
        public FileRepository(IDbFactory dbFactory) : base(dbFactory) { }
    }
}
