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
    public interface ITopicCouncilService
    {
        TopicCouncil Add(TopicCouncil themeCouncil);
        void Update(TopicCouncil themeCouncil);
        TopicCouncil Delete(int id);
        TopicCouncil GetById(int id);
        void SaveChange();
        IEnumerable<TopicCouncil> GetAll();
        PaginationSet<TopicCouncils> GetAllPage(int page, int pageSize);
        PaginationSet<TopicCouncils> GetByIdCouncil(int id, int page, int pageSize);
    }
    public class TopicCouncilService : ITopicCouncilService
    {
        private readonly ITopicCouncilRepository _topicCouncilRepository;
        private readonly FDbContext db;
        private readonly IUnitOfWork _unitOfWork;

        public TopicCouncilService(ITopicCouncilRepository topicCouncilRepository, IUnitOfWork unitOfWork, FDbContext fDbContext)
        {
            _topicCouncilRepository = topicCouncilRepository;
            db = fDbContext;
            _unitOfWork = unitOfWork;
        }

        public PaginationSet<TopicCouncils> GetByIdCouncil(int id, int page, int pageSize)
        {
            var query = from T1 in db.TopicCouncils
                        where T1.TopicId == id
                        select new TopicCouncils
                        {
                            Id = T1.Id,
                            TopicId = T1.Topic.Id,
                            CouncilId = T1.Council.Id,
                            NameCouncil = T1.Council.NameCouncil,
                            NameTopic = T1.Topic.NameTopic,
                            Description = T1.Council.Description
                        };
            var model = query.ToList();
            var result = GetPagedResultForQuery(model, page, pageSize);
            return result;
        }
        public PaginationSet<TopicCouncils> GetAllPage(int page, int pageSize)
        {
            var query = from T1 in db.TopicCouncils
                        select new TopicCouncils
                        {
                            Id = T1.Id,
                            TopicId = T1.Topic.Id,
                            CouncilId = T1.Council.Id,
                            NameCouncil = T1.Council.NameCouncil,
                            NameTopic = T1.Topic.NameTopic
                        };
            var model = query.ToList();
            var result = GetPagedResultForQuery(model, page, pageSize);
            return result;
        }
        private static PaginationSet<TopicCouncils> GetPagedResultForQuery(
       IEnumerable<TopicCouncils> query, int page, int pageSize)
        {
            var result = new PaginationSet<TopicCouncils>();
            result.PageNo = page;
            result.PageSize = pageSize;
            result.TotalCount = query.Count();
            var pageCount = (double)result.TotalCount / pageSize;
            result.Total = (int)Math.Ceiling(pageCount);
            var skip = (page - 1) * pageSize;
            result.Items = query.Skip(skip).Take(pageSize).ToList();
            return result;
        }
        public TopicCouncil Add(TopicCouncil topicCouncil)
        {
            return _topicCouncilRepository.Add(topicCouncil);
        }

        public TopicCouncil Delete(int id)
        {
            return _topicCouncilRepository.Delete(id);
        }

        public IEnumerable<TopicCouncil> GetAll()
        {
            return _topicCouncilRepository.GetAll();
        }
        public TopicCouncil GetById(int id)
        {
            return _topicCouncilRepository.GetSingleById(id);
        }

        public void SaveChange()
        {
            _unitOfWork.Commit();
        }

        public void Update(TopicCouncil topicCouncil)
        {
            _topicCouncilRepository.Update(topicCouncil);
        }
    }
}
