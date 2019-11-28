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
    public interface ITopicService
    {
        Topic Add(Topic topic);
        void Update(Topic topic);
        Topic Delete(int id);
        Topic UpdatePeriodId(int? idPeriod, int id);
        IEnumerable<Topic> GetAll();
        IEnumerable<Topic> Searchs(string searchString);
        Period PassTopic(int? idPeriod, int id);

        PaginationSet<Topics> GetAllPage(int page, int pageSize);
        PaginationSet<Topics> GetByIdPeriod(int id, int page, int pageSize);
        PaginationSet<Topics> GetByIdSchool(int id, int page, int pageSize);
        Topic GetById(int id);
        Topic GetLastId();
        IEnumerable<Topics> GetEditById(int id);
        void SaveChange();
    }
    public class TopicService:ITopicService
    {
        private readonly ITopicRepository _topicRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly FDbContext db;
        public TopicService(ITopicRepository topicRepository,IUnitOfWork unitOfWork,FDbContext fDbContext)
        {
            _topicRepository = topicRepository;
            _unitOfWork = unitOfWork;
            db = fDbContext;
        }
        public PaginationSet<Topics> GetByIdPeriod(int id , int page, int pageSize)
        {
            var query = from T1 in db.Topics
                        where T1.PeriodId == id
                        select new Topics
                        {
                            Id = T1.Id,
                            UserId = T1.UserId,
                            NameUser = T1.User.FullName,
                            NameTopic = T1.NameTopic,
                            SchoolId = T1.SchoolId,
                            NameSchool = T1.School.NameSchool,
                            PeriodId = T1.PeriodId,
                            NamePeriod = T1.Period.Caption,
                            DecisionId = T1.DecisionId,
                            NameDecision = T1.Decision.NameDecision,
                        };
            var results = query.ToList();
            var result = GetPagedResultForQuery(results, page, pageSize);
            return result;
        }
        public PaginationSet<Topics> GetByIdSchool(int id, int page, int pageSize)
        {
            var query = from T1 in db.Topics
                        where T1.SchoolId == id
                        select new Topics
                        {
                            Id = T1.Id,
                            UserId = T1.UserId,
                            NameUser = T1.User.FullName,
                            NameTopic = T1.NameTopic,
                            SchoolId = T1.SchoolId,
                            NameSchool = T1.School.NameSchool,
                            PeriodId = T1.PeriodId,
                            NamePeriod = T1.Period.Caption,
                            DecisionId = T1.DecisionId,
                            NameDecision = T1.Decision.NameDecision,
                        };
            var results = query.ToList();
            var result = GetPagedResultForQuery(results, page, pageSize);
            return result;
        }
        public IEnumerable<Topic> Searchs(string searchString)
        {
            var model = _topicRepository.GetAll();
            if (!String.IsNullOrEmpty(searchString))
            {
                //System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex("\\p{IsCombiningDiacriticalMarks}+");
                //string temp = searchString.Normalize(NormalizationForm.FormD);
                //var value = regex.Replace(temp, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
                model = model.Where(s => s.NameTopic.Contains(searchString));
            }
            return model.ToList();
        }
        public PaginationSet<Topics> GetAllPage(int page, int pageSize)
        {
            var query = from T1 in db.Topics
                        select new Topics
                        {
                            Id = T1.Id,
                            UserId = T1.UserId,
                            NameUser = T1.User.FullName,
                            NameTopic = T1.NameTopic,
                            SchoolId = T1.SchoolId,
                            NameSchool = T1.School.NameSchool,
                            PeriodId = T1.PeriodId,
                            NamePeriod = T1.Period.Caption,
                            DecisionId = T1.DecisionId,
                            NameDecision = T1.Decision.NameDecision,
                        };
            var results = query.ToList();
            var result = GetPagedResultForQuery(results, page, pageSize);
            return result;
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
        public Topic UpdatePeriodId(int? idPeriod, int id)
        {
            var items = db.Topics.Where(w => w.Id == id);
            foreach(var item in items)
            {
                item.PeriodId = idPeriod;
            }
            return null;
        }
        public Period PassTopic(int? idPeriod, int id)
        {
            var items = db.Topics.Where(w => w.Id == id);
            foreach (var item in items)
            {
                item.PeriodId = idPeriod + 1;
            }
            return null;
        }
        public Topic Add(Topic topic)
        {
            return _topicRepository.Add(topic);
        }

        public Topic Delete(int id)
        {
            return _topicRepository.Delete(id);
        }

        public IEnumerable<Topic> GetAll()
        {
            return _topicRepository.GetAll();
        }
        public Topic GetById(int id)
        {
            return _topicRepository.GetSingleById(id);
        }
        public Topic GetLastId()
        {
            var id = db.Topics.OrderByDescending(i => i.Id).First();
            return id;
        }

        public IEnumerable<Topics> GetEditById(int id)
        {
            var query = from T1 in db.Topics
                        join T2 in db.Files on T1.Id equals T2.TopicId
                        where T1.Id == id
                        select new Topics
                        {
                            Id = T1.Id,
                            UserId = T1.UserId,
                            NameUser = T1.User.FullName,
                            NameTopic = T1.NameTopic,
                            NameCategory = T1.Category.NameCategory,
                            CategoryId = T1.Category.Id,
                            SchoolId = T1.SchoolId,
                            NameSchool = T1.School.NameSchool,
                            PeriodId = T1.PeriodId,
                            NamePeriod = T1.Period.Caption,
                            DecisionId = T1.DecisionId,
                            NameDecision = T1.Decision.NameDecision,
                            NameFile = T2.NameFile,
                            Description = T1.Description
                        };
            var results = query.ToList();
            return results;
            //return _topicRepository.GetSingleById(id);
        }
        public void SaveChange()
        {
            _unitOfWork.Commit();
        }

        public void Update(Topic topic)
        {
            _topicRepository.Update(topic);
        }
    }
}
