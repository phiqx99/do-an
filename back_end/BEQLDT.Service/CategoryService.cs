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
    public interface ICategoryService
    {
        Category Add(Category category);
        void Update(Category category);
        Category Delete(int id);
        Category GetById(int id);
        PaginationSet<Topics> GetTopicByCategoryId(int id, int page, int pageSize);
        void SaveChange();
        IEnumerable<Category> Searchs(string searchString);

        bool CheckDelete(int id);
        IEnumerable<Category> GetAll();
        PaginationSet<Category> GetAllPage(int page, int pageSize);
    }
    public class CategoryService:ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly FDbContext db;
        public CategoryService(ICategoryRepository categoryRepository,IUnitOfWork unitOfWork,FDbContext fDbContext)
        {
            _categoryRepository = categoryRepository;
            _unitOfWork = unitOfWork;
            db = fDbContext;
        }
        public bool CheckDelete(int id)
        {
            var categoryTopic = db.Topics.FirstOrDefault(n => n.CategoryId == id);
            if (categoryTopic == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public PaginationSet<Topics> GetTopicByCategoryId(int id, int page, int pageSize)
        {
            var models = from T1 in db.Topics
                        join T2 in db.Categorys on T1.CategoryId equals T2.Id
                        where T1.CategoryId == id
                        select new Topics
                        {
                            NameTopic = T1.NameTopic,
                            UserId = T1.UserId,
                            FullName = T1.User.FullName
                        };
            var model = GetPagedResultForQuery(models, page, pageSize);
            return model;

        }
        private static PaginationSet<Topics> GetPagedResultForQuery(
      IEnumerable<Topics> query, int page, int pageSize)
        {
            var result = new PaginationSet<Topics>();
            result.PageNo = page;
            result.PageSize = pageSize;
            result.TotalCount = query.Count();
            var pageCount = (double)result.TotalCount / pageSize;
            result.Total = (int)Math.Ceiling(pageCount);
            var skip = (page - 1) * pageSize;
            result.Items = query.Skip(skip).Take(pageSize).ToList();
            return result;
        }
        public PaginationSet<Category> GetAllPage(int page, int pageSize)
        {
            var results = _categoryRepository.GetAll();
            var result = GetPagedResultForQuery(results, page, pageSize);
            return result;
        }
        private static PaginationSet<Category> GetPagedResultForQuery(
       IEnumerable<Category> query, int page, int pageSize)
        {
            var result = new PaginationSet<Category>();
            result.PageNo = page;
            result.PageSize = pageSize;
            result.TotalCount = query.Count();
            var pageCount = (double)result.TotalCount / pageSize;
            result.Total = (int)Math.Ceiling(pageCount);
            var skip = (page - 1) * pageSize;
            result.Items = query.Skip(skip).Take(pageSize).ToList();
            return result;
        }
        public IEnumerable<Category> Searchs(string searchString)
        {
            var model = _categoryRepository.GetAll();
            if (!String.IsNullOrEmpty(searchString))
            {
                model = model.Where(s => s.NameCategory.Contains(searchString));
            }
            return model.ToList();
        }
        public Category Add(Category category)
        {
            return _categoryRepository.Add(category);
        }

        public Category Delete(int id)
        {
            return _categoryRepository.Delete(id);
        }

        public IEnumerable<Category> GetAll()
        {
            return _categoryRepository.GetAll();
        }
        public Category GetById(int id)
        {
            return _categoryRepository.GetSingleById(id);
        }

        public void SaveChange()
        {
            _unitOfWork.Commit();
        }

        public void Update(Category category)
        {
            _categoryRepository.Update(category);
        }
    }
}
