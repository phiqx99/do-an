using BEQLDT.Data.Infrastructure;
using BEQLDT.Model;


namespace BEQLDT.Data.Repositories
{
    public interface IPeriodRepository : IRepository<Period> { }
    public class PeriodRepository:RepositoryBase<Period>,IPeriodRepository
    {
        public PeriodRepository(IDbFactory dbFactory) : base(dbFactory) { }
    }
}
