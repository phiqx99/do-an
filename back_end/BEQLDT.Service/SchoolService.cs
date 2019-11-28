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
    public interface ISchoolService
    {
        School Add(School school);
        void Update(School school);
        School Delete(int id);
        IEnumerable<School> Search(string searchString);

        IEnumerable<School> GetAll();
        PaginationSet<School> GetAllPage(int page, int pageSize);
        School GetById(int id);
        bool CheckDelete(int id);
        void SaveChange();
    }
    public class SchoolService:ISchoolService
    {
        private readonly ISchoolRepository _schoolRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly FDbContext db;

        public SchoolService(ISchoolRepository schoolRepository,IUnitOfWork unitOfWork, FDbContext fDbContext)
        {
            _schoolRepository = schoolRepository;
            _unitOfWork = unitOfWork;
            db = fDbContext;
        }

        public bool CheckDelete(int id)
        {
            var topicAll = db.Topics.FirstOrDefault(n=>n.SchoolId==id);
            if (topicAll==null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public IEnumerable<School> Search(string searchString)
        {
            var getAll = _schoolRepository.GetAll();
            var model = from s in getAll
                        select new School
                        {
                            Id = s.Id,
                            NameSchool = s.NameSchool,
                            Description = s.Description,
                            Phone = s.Phone,
                            Email = s.Email,
                            Address = s.Address
                        };
            if (!String.IsNullOrEmpty(searchString))
            {
                model = model.Where(s => s.NameSchool.Contains(searchString));
            }
            return model.ToList();

        }
        public PaginationSet<School> GetAllPage(int page, int pageSize)
        {
            var results = _schoolRepository.GetAll();
            var result = GetPagedResultForQuery(results, page, pageSize);
            return result;
        }
        private static PaginationSet<School> GetPagedResultForQuery(
       IEnumerable<School> query, int page, int pageSize)
        {
            var result = new PaginationSet<School>();
            result.PageNo = page;
            result.PageSize = pageSize;
            result.TotalCount = query.Count();
            var pageCount = (double)result.TotalCount / pageSize;
            result.Total = (int)Math.Ceiling(pageCount);
            var skip = (page - 1) * pageSize;
            result.Items = query.Skip(skip).Take(pageSize).ToList();
            return result;
        }
        public School Add(School school)
        {
            return _schoolRepository.Add(school);
        }

        public School Delete(int id)
        {
            return _schoolRepository.Delete(id);
        }

        public IEnumerable<School> GetAll()
        {
            return _schoolRepository.GetAll();
        }
        public School GetById(int id)
        {
            return _schoolRepository.GetSingleById(id);
        }

        public void SaveChange()
        {
            _unitOfWork.Commit();
        }

        public void Update(School school)
        {
            _schoolRepository.Update(school);
        }
    }
}
