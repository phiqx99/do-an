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
    public interface IDecisionService
    {
        Decision Add(Decision decision);
        void Update(Decision decision);
        Decision Delete(int id);
        IEnumerable<Decision> GetAll();
        Decision GetById(int id);
        void SaveChange();
        bool CheckDelete(int id);
        PaginationSet<Decision> GetAllPage(int page, int pageSize);
    }
    public class DecisionService : IDecisionService
    {
        private readonly IDecisionRepository _decisionRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly FDbContext db;
        
        public DecisionService(IDecisionRepository decisionRepository, IUnitOfWork unitOfWork, FDbContext fDbContext)
        {
            _decisionRepository = decisionRepository;
            _unitOfWork = unitOfWork;
            db = fDbContext;
        }

        public bool CheckDelete(int id)
        {
            var topicAll = db.Topics.FirstOrDefault(n => n.DecisionId == id);
            if (topicAll==null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public PaginationSet<Decision> GetAllPage(int page, int pageSize)
        {
            var results = _decisionRepository.GetAll();
            var result = GetPagedResultForQuery(results, page, pageSize);
            return result;
        }
        private static PaginationSet<Decision> GetPagedResultForQuery(
       IEnumerable<Decision> query, int page, int pageSize)
        {
            var result = new PaginationSet<Decision>();
            result.PageNo = page;
            result.PageSize = pageSize;
            result.TotalCount = query.Count();
            var pageCount = (double)result.TotalCount / pageSize;
            result.Total = (int)Math.Ceiling(pageCount);
            var skip = (page - 1) * pageSize;
            result.Items = query.Skip(skip).Take(pageSize).ToList();
            return result;
        }

        public Decision Add(Decision decision)
        {
            return _decisionRepository.Add(decision);
        }

        public Decision Delete(int id)
        {
            return _decisionRepository.Delete(id);
        }

        public IEnumerable<Decision> GetAll()
        {
            return _decisionRepository.GetAll();
        }


        public Decision GetById(int id)
        {
            return _decisionRepository.GetSingleById(id);
        }

        public void SaveChange()
        {
            _unitOfWork.Commit();
        }

        public void Update(Decision decision)
        {
            _decisionRepository.Update(decision);
        }
    }
}
