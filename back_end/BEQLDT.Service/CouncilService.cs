using BEQLDT.Data;
using BEQLDT.Data.Infrastructure;
using BEQLDT.Data.Repositories;
using BEQLDT.Model;
using BEQLDT.Service.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BEQLDT.Service
{
    public interface ICouncilService
    {
        Council Add(Council council);
        void Update(Council council);
        Council Delete(int id);
        IEnumerable<Council> GetAll();
        IEnumerable<Council> Searchs(string searchString);
        Council GetById(int id);
        bool CheckDelete(int id);
        void SaveChange();
        
        PaginationSet<Council> GetAllPage(int page, int pageSize);
    }
    public class CouncilService : ICouncilService
    {
        private readonly ICouncilRepository _councilRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly FDbContext db;

        public CouncilService(ICouncilRepository councilRepository, IUnitOfWork unitOfWork, FDbContext fDbContext)
        {
            this._councilRepository = councilRepository;
            this._unitOfWork = unitOfWork;
            db = fDbContext;
        }

        public bool CheckDelete(int id)
        {
            var council = db.GroupUsers.FirstOrDefault(n => n.CouncilId == id);
            var topicCouncil = db.TopicCouncils.FirstOrDefault(n => n.CouncilId == id);
            if (council == null || topicCouncil == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public IEnumerable<Council> Searchs( string searchString)
        {
            var model = _councilRepository.GetAll();
            if (!String.IsNullOrEmpty(searchString))
            {
                model = model.Where(s => s.NameCouncil.Contains(searchString));
            }
            return model.ToList();
        }

        public PaginationSet<Council> GetAllPage(int page, int pageSize)
        {
            var results = _councilRepository.GetAll();
            var result = GetPagedResultForQuery(results, page, pageSize);
            return result;
        }
        private static PaginationSet<Council> GetPagedResultForQuery(
       IEnumerable<Council> query, int page, int pageSize)
        {
            var result = new PaginationSet<Council>();
            result.PageNo = page;
            result.PageSize = pageSize;
            result.TotalCount = query.Count();
            var pageCount = (double)result.TotalCount / pageSize;
            result.Total = (int)Math.Ceiling(pageCount);
            var skip = (page - 1) * pageSize;
            result.Items = query.Skip(skip).Take(pageSize).ToList();
            return result;
        }

        public Council Add(Council council)
        {
            return _councilRepository.Add(council);
        }

        public Council Delete(int id)
        {
            return _councilRepository.Delete(id);
        }

        public IEnumerable<Council> GetAll()
        {
            return _councilRepository.GetAll();
        }


        public Council GetById(int id)
        {
            return _councilRepository.GetSingleById(id);
        }

        public void SaveChange()
        {
            _unitOfWork.Commit();
        }

        public void Update(Council council)
        {
            _councilRepository.Update(council);
        }
    }
}
