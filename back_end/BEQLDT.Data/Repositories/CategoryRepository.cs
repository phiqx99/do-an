using BEQLDT.Data.Infrastructure;
using BEQLDT.Model;
namespace BEQLDT.Data.Repositories
{
    public interface ICategoryRepository : IRepository<Category> { }
    public class CategoryRepository:RepositoryBase<Category>,ICategoryRepository
    {
        public CategoryRepository(IDbFactory dbFactory) : base(dbFactory) { }
    }
}
