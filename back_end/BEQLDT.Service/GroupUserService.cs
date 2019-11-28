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
    public interface IGroupUserService
    {
        GroupUser Add(GroupUser groupUser);
        void Update(GroupUser groupUser);
        GroupUser Delete(int id);
        IEnumerable<GroupUser> GetAll();
        PaginationSet<GroupUser> GetAllPage(int page, int pageSize);
        GroupUser GetById(int id);
        PaginationSet<GroupUsers> GetByIdUserGroup(int Id, int page, int pageSize);
        PaginationSet<GroupUsers> GetByIdUserCouncil(int Id, int page, int pageSize);
        void SaveChange();
    }
    public class GroupUserService : IGroupUserService
    {
        private readonly IGroupUserRepository _groupUserRepository;
        private readonly FDbContext db;
        private readonly IUnitOfWork _unitOfWork;
       
        public GroupUserService(IGroupUserRepository groupUserRepository, IUnitOfWork unitOfWork, FDbContext fDbContext)
        {
            this._groupUserRepository = groupUserRepository;
            this._unitOfWork = unitOfWork;
            db = fDbContext;
          
    }


        public PaginationSet<GroupUsers> GetByIdUserCouncil(int Id, int page, int pageSize)
        {

            var query = from T1 in db.GroupUsers
                        where T1.CouncilId == Id
                        select new GroupUsers()
                        {
                            Id = T1.Id,
                            Username = T1.User.Username,
                            UserId = T1.UserId,

                        };

            var model = query.ToList();
            var result = GetPagedResultForQuery(model, page, pageSize);
            return result;
        }

        public PaginationSet<GroupUsers> GetByIdUserGroup(int Id, int page, int pageSize)
        {
            
            var query = from T1 in db.GroupUsers
                        where T1.GroupId==Id
                        select new GroupUsers()
                        {
                            Id = T1.Id,
                            Username = T1.User.Username,
                            NameGroup = T1.Group.NameGroup,
                            UserId = T1.UserId,
                            GroupId = T1.GroupId
                            
                        };

            var model = query.ToList();
            var result = GetPagedResultForQuery(model, page, pageSize);
            return result;
        }

        private static PaginationSet<GroupUsers> GetPagedResultForQuery(
    IEnumerable<GroupUsers> query, int page, int pageSize)
        {
            var result = new PaginationSet<GroupUsers>();
            result.PageNo = page;
            result.PageSize = pageSize;
            result.TotalCount = query.Count();
            var pageCount = (double)result.TotalCount / pageSize;
            result.Total = (int)Math.Ceiling(pageCount);
            var skip = (page - 1) * pageSize;
            result.Items = query.Skip(skip).Take(pageSize).ToList();
            return result;
        }

        public PaginationSet<GroupUser> GetAllPage(int page, int pageSize)
        {
            var results = _groupUserRepository.GetAll();
            var result = GetPagedResultForQuery(results, page, pageSize);
            return result;
        }
        private static PaginationSet<GroupUser> GetPagedResultForQuery(
        IEnumerable<GroupUser> query, int page, int pageSize)
        {
            var result = new PaginationSet<GroupUser>();
            result.PageNo = page;
            result.PageSize = pageSize;
            result.TotalCount = query.Count();
            var pageCount = (double)result.TotalCount / pageSize;
            result.Total = (int)Math.Ceiling(pageCount);
            var skip = (page - 1) * pageSize;
            result.Items = query.Skip(skip).Take(pageSize).ToList();
            return result;
        }
        public GroupUser Add(GroupUser groupUser)
        {
            return _groupUserRepository.Add(groupUser);
        }

        public GroupUser Delete(int id)
        {
            return _groupUserRepository.Delete(id);
        }

        public IEnumerable<GroupUser> GetAll()
        {
            return _groupUserRepository.GetAll();
        }

        public GroupUser GetById(int id)
        {
            return _groupUserRepository.GetSingleById(id);
        }

        public void SaveChange()
        {
            _unitOfWork.Commit();
        }

        public void Update(GroupUser groupUser)
        {
            _groupUserRepository.Update(groupUser);
        }
    }
}
