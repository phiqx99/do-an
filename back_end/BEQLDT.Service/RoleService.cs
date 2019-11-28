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
    public interface IRoleService
    {
        Role Add(Role role);
        void Update(Role role);
        Role Delete(int id);
        IEnumerable<Role> GetAll();
        PaginationSet<Role> GetAllPage(int page, int pageSize);
        Role GetById(int id);
        bool CheckDelete(int id);
        void SaveChange();
    }
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly FDbContext db;

        public RoleService(IRoleRepository roleRepository, IUnitOfWork unitOfWork,FDbContext fDbContext)
        {
            this._roleRepository = roleRepository;
            this._unitOfWork = unitOfWork;
            db = fDbContext;
        }

        public bool CheckDelete(int id)
        {
            var per = db.Permissions.FirstOrDefault(n => n.RoleId == id);
            if (per == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public PaginationSet<Role> GetAllPage(int page, int pageSize)
        {
            var results = _roleRepository.GetAll();
            var result = GetPagedResultForQuery(results, page, pageSize);
            return result;
        }
        private static PaginationSet<Role> GetPagedResultForQuery(
       IEnumerable<Role> query, int page, int pageSize)
        {
            var result = new PaginationSet<Role>();
            result.PageNo = page;
            result.PageSize = pageSize;
            result.TotalCount = query.Count();
            var pageCount = (double)result.TotalCount / pageSize;
            result.Total = (int)Math.Ceiling(pageCount);
            var skip = (page - 1) * pageSize;
            result.Items = query.Skip(skip).Take(pageSize).ToList();
            return result;
        }
        public Role Add(Role role)
        {
            return _roleRepository.Add(role);
        }

        public Role Delete(int id)
        {
            return _roleRepository.Delete(id);
        }

        public IEnumerable<Role> GetAll()
        {
            return _roleRepository.GetAll();
        }

        public Role GetById(int id)
        {
            return _roleRepository.GetSingleById(id);
        }

        public void SaveChange()
        {
            _unitOfWork.Commit();
        }

        public void Update(Role role)
        {
            _roleRepository.Update(role);
        }
    }
}
