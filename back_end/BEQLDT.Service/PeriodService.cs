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
    public interface IPeriodService
    {
        Period Add(Period period);
        void Update(Period period);
        Period Delete(int id);
        IEnumerable<Period> GetAll();
        Period GetById(int id);
        void SaveChange();
        bool CheckDelete(int id);
        PaginationSet<Period> GetAllPage(int page, int pageSize);
    }
    public class PeriodService:IPeriodService
    {
        private readonly IPeriodRepository _periodRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly FDbContext db;
        

        public PeriodService(IPeriodRepository periodRepository,IUnitOfWork unitOfWork, FDbContext fDbContext)
        {
            _periodRepository = periodRepository;
            _unitOfWork = unitOfWork;
            db = fDbContext;
        }

        public bool CheckDelete(int id)
        {
            var topicAll = db.Topics.FirstOrDefault(n=>n.PeriodId==id);
            if (topicAll == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public PaginationSet<Period> GetAllPage(int page, int pageSize)
        {
            var results = _periodRepository.GetAll();
            var result = GetPagedResultForQuery(results, page, pageSize);
            return result;
        }
        private static PaginationSet<Period> GetPagedResultForQuery(
       IEnumerable<Period> query, int page, int pageSize)
        {
            var result = new PaginationSet<Period>();
            result.PageNo = page;
            result.PageSize = pageSize;
            result.TotalCount = query.Count();
            var pageCount = (double)result.TotalCount / pageSize;
            result.Total = (int)Math.Ceiling(pageCount);
            var skip = (page - 1) * pageSize;
            result.Items = query.Skip(skip).Take(pageSize).ToList();
            return result;
        }
        

        public Period Add(Period period)
        {
            return _periodRepository.Add(period);
        }

        public Period Delete(int id)
        {
            return _periodRepository.Delete(id);
        }

        public IEnumerable<Period> GetAll()
        {
            return _periodRepository.GetAll();
        }


        public Period GetById(int id)
        {
            return _periodRepository.GetSingleById(id);
        }

        public void SaveChange()
        {
            _unitOfWork.Commit();
        }

        public void Update(Period period)
        {
            _periodRepository.Update(period);
        }
    }
}
