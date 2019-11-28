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
    public class GroupUserController : Controller
    {

        private readonly IGroupUserService _groupUserService;
        private readonly IGroupUserRepository _groupUserRepository;
   

        public GroupUserController(IGroupUserService groupUserService, IGroupUserRepository groupUserRepository)
        {
            this._groupUserService = groupUserService;
            this._groupUserRepository = groupUserRepository;
           
        }

        #region Created Group User
        [HttpPost]
        [Route("Created")]
        public IActionResult CreatedGroupUser(GroupUserViewModel groupUserVM)
        {
            var result = new ResultApi(false);
            if (!ModelState.IsValid)
            {
                result.ErrorCode = (int)ErrorCode.DataInvalid;
                result.Message = "Dữ liệu không hợp lệ";
            }
            if(_groupUserRepository.CheckContains(n=>n.GroupId == groupUserVM.GroupId 
                && n.UserId==groupUserVM.UserId) )
            {
                result.ErrorCode=(int)ErrorCode.ObjectExist;
                result.Message = "Đã tồn tại";
            }
            else if (_groupUserRepository.CheckContains(n => n.GroupId == groupUserVM.GroupId
                && n.UserId != groupUserVM.UserId || n.GroupId != groupUserVM.GroupId))
            {
                var groupUserModel = new GroupUser();
                var claimIdUser = HttpContext.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
                int idUser = Convert.ToInt32(claimIdUser);
                groupUserVM.Active = true;
                groupUserVM.CreatedAt = DateTime.Now;
                groupUserVM.CreatedUser = idUser;
                groupUserVM.UpdateAt = groupUserModel.UpdateAt;
                groupUserVM.UpdateUser = groupUserModel.UpdateUser;

                groupUserModel.UpdateGroupUserModel(groupUserVM);
                _groupUserService.Add(groupUserModel);
                _groupUserService.SaveChange();
                result.Success = true;
                result.Data = groupUserModel;
            }
            return Ok(result);
        }

        #endregion

        #region Update Group User
        [HttpPut]
        [Route("Update")]
        public IActionResult UpdateGroupUser(GroupUserViewModel groupUserVM, int Id)
        {
            var result = new ResultApi(false);
            var groupUserModel = _groupUserService.GetById(Id);
            if (groupUserModel == null)
            {
                result.ErrorCode = (int)ErrorCode.GroupUserNotFound;
                result.Message = "GroupUser rỗng";
            }
            var claimIdUser = HttpContext.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            int idUser = Convert.ToInt32(claimIdUser);
            groupUserVM.Active = true;
            groupUserVM.UpdateAt = DateTime.Now;
            groupUserVM.UpdateUser = idUser;
            groupUserVM.CreatedAt = groupUserModel.CreatedAt;
            groupUserVM.CreatedUser = groupUserModel.CreatedUser;

            groupUserModel.UpdateGroupUserModel(groupUserVM);
                _groupUserService.Update(groupUserModel);
                _groupUserService.SaveChange();
                result.Success = true;
                result.Data = groupUserModel;

            return Ok(result);

        }

        #endregion

        #region Delete Group User
        [HttpDelete]
        [Route("Delete")]
        public IActionResult DeleteGroupUser(int id)
        {
            var result = new ResultApi(false);
            if(_groupUserRepository.CheckContains(n => n.Id == id))
            {
                _groupUserService.Delete(id);
                _groupUserService.SaveChange();
                result.Success = true;
                result.Message = "Delete Success";
            }
            else
            {
                result.ErrorCode = (int)ErrorCode.DeleteFailed;
            }
            return Ok(result);

        }
        #endregion

        #region Get Group User
        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetAllGroupUser(int page = 1, int pageSize = 10)
        {
            var groupUser = _groupUserService.GetAllPage(page, pageSize);
            var result = new ResultApi(false);
            if (groupUser != null)
            {
                result.Success = true;
                result.Data = groupUser;
            }
            else
            {
                result.ErrorCode = (int)ErrorCode.GroupUserNotFound;
                result.Message = "Không có dữ liệu";
            }
            return Ok(result);
        }
        [HttpGet]
        [Route("GetById")]
        public IActionResult GetId(int id)
        {
            var result = new ResultApi(false);
            var groupUser = _groupUserService.GetById(id);
            if (groupUser != null)
            {
                result.Success = true;
                result.Data = groupUser;
            }
            else
            {
                result.ErrorCode = (int)ErrorCode.GroupUserNotFound;
                result.Message = "Không có dữ liệu";
            }
            return Ok(result);
        }
        [HttpGet]
        [Route("GetByGroupId")]
        public IActionResult GetGroupUserId(int Id, int page = 1, int pageSize = 10)
        {
            var result = new ResultApi(false);
            var groupUser = _groupUserService.GetByIdUserGroup(Id, page, pageSize);
            
            if(groupUser != null)
            {
                result.Data =  groupUser ;
            }
            return Ok(result);

        }

        [HttpGet]
        [Route("GetByCouncilId")]
        public IActionResult GetCoucilUserId(int Id, int page = 1, int pageSize = 10)
        {
            var result = new ResultApi(false);
            var groupUser = _groupUserService.GetByIdUserCouncil(Id, page, pageSize);

            if (groupUser != null)
            {
                result.Data = groupUser;
            }
            return Ok(result);

        }

        #endregion
    }
}