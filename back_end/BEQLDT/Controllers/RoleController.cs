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
    public class RoleController : Controller
    {
        private readonly IRoleService _roleService;
        private readonly IRoleRepository _roleRepository;


        public RoleController(IRoleService roleService, IRoleRepository roleRepository)
        {
            this._roleRepository = roleRepository;
            this._roleService = roleService;
        }


        #region Created Role User
        [HttpPost]
        [Route("Created")]
        public IActionResult CreatedRole( RoleViewModel roleVM)
        {
            var result = new ResultApi(false);
            if (!ModelState.IsValid)
            {
                result.ErrorCode = (int)ErrorCode.DataInvalid;
                result.Message = "Dữ liệu không hợp lệ";
            }
            else
            {

                var roleModel = new Role();
                var claimIdUser = HttpContext.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
                int idUser = Convert.ToInt32(claimIdUser);
                roleVM.Active = true;
                roleVM.CreatedAt = DateTime.Now;
                roleVM.CreatedUser = idUser;
                roleVM.UpdateAt = roleModel.UpdateAt;
                roleVM.UpdateUser = roleModel.UpdateUser;

                roleModel.UpdateRoleModel(roleVM);
                _roleService.Add(roleModel);
                _roleService.SaveChange();

                result.Success = true;
                result.Data = roleModel;
            }
            return Ok(result);
        }

        #endregion

        #region Update Role User
        [HttpPut]
        [Route("Update")]
        public IActionResult UpdateRole(RoleViewModel roleVM, int Id)
        {
            var result = new ResultApi(false);
            var roleModel = _roleService.GetById(Id);
            if (roleModel == null)
            {
                result.ErrorCode = (int)ErrorCode.RoleNotFound;
                result.Message = "Role rỗng";
            }
            else
            {
                var claimIdUser = HttpContext.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
                int idUser = Convert.ToInt32(claimIdUser);
                roleVM.Active = true;
                roleVM.UpdateAt = DateTime.Now;
                roleVM.UpdateUser = idUser;
                roleVM.CreatedAt = roleModel.CreatedAt;
                roleVM.CreatedUser = roleModel.CreatedUser;

                roleModel.UpdateRoleModel(roleVM);
                _roleService.Update(roleModel);
                _roleService.SaveChange();

                result.Success = true;
                result.Data = roleModel;
            }
            return Ok(result);

        }

        #endregion

        #region Delete Role User
        [HttpDelete]
        [Route("Delete")]
        public IActionResult DeleteRole(int id)
        {

            var result = new ResultApi(false);
            var checkDel = _roleService.CheckDelete(id);
            if (checkDel==true)
            {
                result.Success = false;
                result.Message = "Role đang được sử dụng";
            }
            else if (_roleRepository.CheckContains(n => n.Id == id))
            {
                result.Success = true;
                _roleService.Delete(id);
                _roleService.SaveChange();
                result.Message = "Xóa thành công ";
            }
            else
            {
                result.ErrorCode = (int)ErrorCode.RoleNotFound;
                result.Message = "Role đang được sử dụng hoặc không tồn tại";
            }
            return Ok(result);

        }
        #endregion

        #region Get Role User
        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetAllRole(int page = 1 ,int pageSize = 10)
        {
            var role = _roleService.GetAllPage(page, pageSize);
            var result = new ResultApi(false);
            if (role != null)
            {
                result.Success = true;
                result.Data = role;
            }
            else
            {
                result.ErrorCode = (int)ErrorCode.RoleNotFound;
                result.Message = "Không có dữ liệu";
            }
            return Ok(result);
        }
        [HttpGet]
        [Route("GetById")]
        public IActionResult GetId(int id)
        {
            var role = _roleService.GetById(id);
            var result = new ResultApi(false);
            if (role != null)
            {
                result.Success = true;
                result.Data = role;
            }
            else
            {
                result.ErrorCode = (int)ErrorCode.RoleNotFound;
                result.Message = "Không có dữ liệu";
            }
            return Ok(result);
        }
        #endregion

    }
}