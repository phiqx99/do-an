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
    public interface IPermissionService
    {
        Permission Add(Permission permission);
        void Update(Permission permission);
        Permission Delete(int id);
        IEnumerable<Permission> GetAll();
        PaginationSet<PerRoles> GetAllPage(int page, int pageSize);
        PaginationSet<PerRoles> GetByIdRole(int id, int page, int pageSize);
        Permission GetById(int id);
        void SaveChange();
    }
    public class PermissionService : IPermissionService
    {
        private readonly IPermissionRepository _permissionRepository;
        private readonly FDbContext db;
        private readonly IUnitOfWork _unitOfWork;

        public PermissionService(IPermissionRepository permissionRepository, IUnitOfWork unitOfWork , FDbContext fDbContext)
        {
            this._permissionRepository = permissionRepository;
            this._unitOfWork = unitOfWork;
            db = fDbContext;
            
           
        }
     
        public PaginationSet<PerRoles> GetByIdRole(int id, int page , int pageSize )
        {
            var query = from T1 in db.Permissions
                        where T1.RoleId == id
                        select new PerRoles
                        {
                            Id = T1.Id,
                            GroupId = T1.Id,
                            RoleId = T1.RoleId,
                            NameGroup = T1.Group.NameGroup,
                            NameRole = T1.Role.NameRole
                        };
            var model = query.ToList();
            var result = GetPagedResultForQuery(model, page, pageSize);
            return result;
        }
        public PaginationSet<PerRoles> GetAllPage( int page, int pageSize)
        {

            var query = from T1 in db.Permissions
                  
                        select new PerRoles()
                        {
                            Id = T1.Id,
                            GroupId = T1.Id,
                            RoleId =T1.RoleId,
                            NameGroup = T1.Group.NameGroup,
                            NameRole = T1.Role.NameRole
                        };

            var model = query.ToList();
            var result = GetPagedResultForQuery(model, page, pageSize);
            return result;
        }

        private static PaginationSet<PerRoles> GetPagedResultForQuery(
    IEnumerable<PerRoles> query, int page, int pageSize)
        {
            var result = new PaginationSet<PerRoles>();
            result.PageNo = page;
            result.PageSize = pageSize;
            result.TotalCount = query.Count();
            var pageCount = (double)result.TotalCount / pageSize;
            result.Total = (int)Math.Ceiling(pageCount);
            var skip = (page - 1) * pageSize;
            result.Items = query.Skip(skip).Take(pageSize).ToList();
            return result;
        }


        public Permission Add(Permission permission)
        {
            return _permissionRepository.Add(permission);
        }

        public Permission Delete(int id)
        {
            return _permissionRepository.Delete(id);
        }

        public IEnumerable<Permission> GetAll()
        {
            return _permissionRepository.GetAll();
        }

        public Permission GetById(int id)
        {
            return _permissionRepository.GetSingleById(id);
        }

        public void SaveChange()
        {
            _unitOfWork.Commit();
        }

        public void Update(Permission permission)
        {
            _permissionRepository.Update(permission);
        }
    }
}
