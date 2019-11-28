using BEQLDT.Data;
using BEQLDT.Data.Infrastructure;
using BEQLDT.Data.Repositories;
using BEQLDT.Model;
using BEQLDT.Service.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BEQLDT.Service
{
    public interface IGroupService
    {
        Group Add(Group group);
        void Update(Group group);
        Group Delete(int id);
        IEnumerable<Group> GetAll();
        IEnumerable<Group> Search(string searchString);
        PaginationSet<Group> GetAllPage(int page, int pageSize);
        Group GetById(int id);
        bool CheckDelete(int id);
        void SaveChange();
    }
    public class GroupService : IGroupService
    {
        private readonly IGroupRepository _groupRepository;
        private readonly FDbContext db;
        private readonly IUnitOfWork _unitOfWork;

        public GroupService(IGroupRepository groupRepository, IUnitOfWork unitOfWork, FDbContext fDbContext)
        {
            this._groupRepository = groupRepository;
            this._unitOfWork = unitOfWork;
            db = fDbContext;
        }
        public bool CheckDelete(int id)
        {
            var per = db.Permissions.FirstOrDefault(n => n.GroupId == id);
            var grU = db.GroupUsers.FirstOrDefault(n => n.GroupId == id);
            if((per == null || grU == null))
            {
                return false;
            }
            else
            {
                return true;
            }
            
        }
        public IEnumerable<Group> Search(string searchString)
        {
            var model = from T1 in db.Groups
                        select new Group
                        {
                            Id = T1.Id,
                            NameGroup = T1.NameGroup
                        };
            if (!String.IsNullOrEmpty(searchString))
            {
                //System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex("\\p{IsCombiningDiacriticalMarks}+");
                //string temp = searchString.Normalize(NormalizationForm.FormD);
                //var value = regex.Replace(temp, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
                model = model.Where(n => n.NameGroup.Contains(searchString));
            }
            return model.ToList();
        }

        public PaginationSet<Group> GetAllPage(int page, int pageSize)
        {
            var results = _groupRepository.GetAll();
            var result = GetPagedResultForQuery(results, page, pageSize);
            return result;
        }
        private static PaginationSet<Group> GetPagedResultForQuery(
       IEnumerable<Group> query, int page, int pageSize)
        {
            var result = new PaginationSet<Group>();
            result.PageNo = page;
            result.PageSize = pageSize;
            result.TotalCount = query.Count();
            var pageCount = (double)result.TotalCount / pageSize;
            result.Total = (int)Math.Ceiling(pageCount);
            var skip = (page - 1) * pageSize;
            result.Items = query.Skip(skip).Take(pageSize).ToList();
            return result;
        }
        public Group Add(Group group)
        {
            return _groupRepository.Add(group);
        }

        public Group Delete(int id)
        {
            return _groupRepository.Delete(id);
        }

        public IEnumerable<Group> GetAll()
        {
            return _groupRepository.GetAll();
        }

        public Group GetById(int id)
        {
            return _groupRepository.GetSingleById(id);
        }

        public void SaveChange()
        {
            _unitOfWork.Commit();
        }

        public void Update(Group group)
        {
            _groupRepository.Update(group);
        }
    }
}
