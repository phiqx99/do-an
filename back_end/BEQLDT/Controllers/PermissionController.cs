using BEQLDT.Data.Repositories;
using BEQLDT.Infrastructure.Extension;
using BEQLDT.Model;
using BEQLDT.Model.Request;
using BEQLDT.Model.Result;
using BEQLDT.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Claims;

namespace BEQLDT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PermissionController : Controller
    {
        private readonly IPermissionService _permissionService;
        private readonly IPermissionRepository _permissionRepository;


        public PermissionController(IPermissionService permissionService, IPermissionRepository permissionRepository)
        {
            this._permissionRepository = permissionRepository;
            this._permissionService = permissionService;
        }

        #region Created Permission
        [HttpPost]
        [Route("Created")]
        public IActionResult CreatedPermission( PermissionViewModel permissionVM)
        {
            var result = new ResultApi(false);
            if (!ModelState.IsValid)
            {
                result.ErrorCode = (int)ErrorCode.DataInvalid;
                result.Message = "Dữ liệu không hợp lệ";
            }
            if (_permissionRepository.CheckContains(n => n.GroupId == permissionVM.GroupId
               && n.RoleId == permissionVM.RoleId))
            {
                result.ErrorCode = (int)ErrorCode.ObjectExist;
                result.Message = "Group và Role đã tồn tại";
            }
            else
            {
                 var permissionModel = new Permission();
                var claimIdUser = HttpContext.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
                int idUser = Convert.ToInt32(claimIdUser);
                permissionVM.Active = true;
                permissionVM.CreatedAt = DateTime.Now;
                permissionVM.CreatedUser = idUser;
                permissionVM.UpdateAt = permissionModel.UpdateAt;
                permissionVM.UpdateUser = permissionModel.UpdateUser;

                permissionModel.UpdatePermissionModel(permissionVM);
                _permissionService.Add(permissionModel);
                _permissionService.SaveChange();

                result.Success = true;
                result.Data = permissionModel;
            }
            return Ok(result);
        }

        #endregion

        #region Update Permission
        [HttpPut]
        [Route("Update")]
        public IActionResult UpdatePermission(PermissionViewModel permissionVM, int Id)
        {
            
            var permissionModel = _permissionService.GetById(Id);
            var result = new ResultApi(false);
            if (permissionModel == null)
            {
                result.ErrorCode = (int)ErrorCode.PermissionNotFound;
                result.Message = "Permission rỗng";
            }else if (permissionModel.RoleId != permissionVM.RoleId)
            {
                result.ErrorCode = (int)ErrorCode.ObjectDontChange;
                result.Message = "Không Thể thay đổi vai trò !";
            }
            else
             {
                var claimIdUser = HttpContext.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
                int idUser = Convert.ToInt32(claimIdUser);
                permissionVM.Active = true;
                permissionVM.UpdateAt = DateTime.Now;
                permissionVM.UpdateUser = idUser;
                permissionVM.CreatedAt = permissionModel.CreatedAt;
                permissionVM.CreatedUser = permissionModel.CreatedUser;

                permissionModel.UpdatePermissionModel(permissionVM);
                _permissionService.Update(permissionModel);
                _permissionService.SaveChange();

                result.Success = true;
                result.Data = permissionModel;
            }
            return Ok(result);

        }

        #endregion

        #region Delete Permission
        [HttpDelete]
        [Route("Delete")]
        public IActionResult DeletePermission(int id)
        {

            var result = new ResultApi(false);
            if (_permissionRepository.CheckContains(n => n.Id == id))
            {
                _permissionService.Delete(id);
                _permissionService.SaveChange();
                result.Message = "Delete Success";
            }
            else
            {
                result.ErrorCode = (int)ErrorCode.DeleteFailed;
            }
            return Ok(result);

        }
        #endregion

        #region Get Permission
        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetAllPermission(int page = 1,int pageSize =10)
        {
            var permission = _permissionService.GetAllPage(page, pageSize);
            var result = new ResultApi(false);
            if (permission != null)
            {
                result.Success = true;
                result.Data = permission;
            }
            else
            {
                result.ErrorCode = (int)ErrorCode.PermissionNotFound;
                result.Message = "Không có dữ liệu";
            }
            return Ok(result);
        }
        [HttpGet]
        [Route("GetByPerId")]
        [AllowAnonymous]
        public IActionResult GetByPerId(int id, int page = 1, int pageSize = 10 )
        {
            var result = new ResultApi(true);
            var model = _permissionService.GetByIdRole(id, page, pageSize);
            if (model != null)
            {
                result.Success = true;
                result.Data = model;
            }
            else
            {
                result.ErrorCode = (int)ErrorCode.PermissionNotFound;
                result.Message = "Không có dữ liệu";
            }
            return Ok(result);
        }

        [HttpGet]
        [Route("GetById")]
        public IActionResult GetId(int id)
        {
           var permission = _permissionService.GetById(id);
            var result = new ResultApi(false);
            if (permission != null)
            {
                result.Success = true;
                result.Data = permission;
            }
            else
            {
                result.ErrorCode = (int)ErrorCode.PermissionNotFound;
                result.Message = "Không có dữ liệu";
            }
            return Ok(result);

        }
        #endregion
    }
}