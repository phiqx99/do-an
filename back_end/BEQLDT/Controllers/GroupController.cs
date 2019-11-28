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
    public class GroupController : Controller
    {
        private readonly IGroupService _groupService;
        private readonly IGroupRepository _groupRepository;

        public GroupController(IGroupService groupService,IGroupRepository groupRepository)
        {
            this._groupRepository = groupRepository;
            this._groupService = groupService;
        }

        #region Created Group
        [HttpPost]
        [Route("Created")]
        public IActionResult CreatedGroup( GroupViewModel groupVM)
        {

            var result = new ResultApi(false);
            if (!ModelState.IsValid)
            {
                result.ErrorCode = (int)ErrorCode.DataInvalid;
                result.Message = "Dữ liệu không hợp lệ";
            }
            else if (_groupRepository.CheckContains(n => n.NameGroup == groupVM.NameGroup))
            {
                result.ErrorCode = (int)ErrorCode.NameAlreadyExists;
                result.Message = "Tên group đã tồn tại";
            }
            else
            {
                var groupModel = new Group();
                var claimIdUser = HttpContext.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
                int idUser = Convert.ToInt32(claimIdUser);
                groupVM.Active = true;
                groupVM.CreatedAt = DateTime.Now;
                groupVM.CreatedUser = idUser;
                groupVM.UpdateAt = groupModel.UpdateAt;
                groupVM.UpdateUser = groupModel.UpdateUser;      
                groupModel.UpdateGroupModel(groupVM);
                _groupService.Add(groupModel);
                _groupService.SaveChange();
                result.Success = true;
                result.Data = groupModel;
                
            }
            return Ok(result);
        }

        #endregion

        #region Update Group
        [HttpPut]
        [Route("Update")]
        public IActionResult UpdateGroup(GroupViewModel groupVM, int Id)
        {
            var result = new ResultApi(false);
            var groupModel = _groupService.GetById(Id);
            if (groupModel == null)
            {
                result.ErrorCode = (int)ErrorCode.GroupNotFound;
                result.Message = "Group không tồn tại";
            }
            else
            {
                var claimIdUser = HttpContext.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
                int idUser = Convert.ToInt32(claimIdUser);
                groupVM.Active = true;
                groupVM.UpdateAt = DateTime.Now;
                groupVM.UpdateUser = idUser;
                groupVM.CreatedAt = groupModel.CreatedAt;
                groupVM.CreatedUser = groupModel.CreatedUser;

                groupModel.UpdateGroupModel(groupVM);
                _groupService.Update(groupModel);
                _groupService.SaveChange();

                result.Success = true;
                result.Data = groupModel;
            }
            return Ok(result);

        }

        #endregion

        #region Delete Group
        [HttpDelete]
        [Route("Delete")]
        [AllowAnonymous]
        public IActionResult DeleteGroup( int id)
        {
            var result = new ResultApi(false);
            var checkId = _groupService.CheckDelete(id);
            if(checkId == true)
            {
                result.Success = false;
                result.Message = "Group đang được sử dụng";
            }
            else if (_groupRepository.CheckContains(n => n.Id == id))
            {

                _groupService.Delete(id);
                _groupService.SaveChange();
                result.Success = true;
                result.Message = "Xóa thành công";
                
            }
            else
            {
                result.ErrorCode = (int)ErrorCode.GroupNotFound;
                result.Message = "Group  không tồn tại";
            }
            return Ok(result);

        }
        #endregion

        #region Get Group
        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetAllGroup(int page = 1 , int pageSize = 10)
        {
            
            var group = _groupService.GetAllPage(page,pageSize);
            var result = new ResultApi(false);
            if (group != null)
            {
                result.Success = true;
                result.Data = group;
            }
            else
            {
                result.ErrorCode = (int)ErrorCode.GroupNotFound;
                result.Message = "Group rỗng";
            }
            return Ok(result);
        }
        [HttpGet]
        [Route("GetById")]
        public IActionResult GetId(int id)
        {
            var group = _groupService.GetById(id);
            var result = new ResultApi(false);
            if (group != null)
            {
                result.Success = true;
                result.Data = group;
            }
            else
            {
                result.ErrorCode = (int)ErrorCode.GroupNotFound;
                result.Message = "Group rỗng";
            }
            return Ok(result);
        }
        #endregion

        #region Search
        [HttpGet]
        [Route("Search")]
        public IActionResult Search(string searchString)
        {
            var result = new ResultApi(true);
            if(searchString != null)
            {
                var model = _groupService.Search(searchString);
                result.Data = model;
            }
            else
            {
                result.Message = "Không tìm thấy Group";
            }
            return Ok(result);
        }
        #endregion

    }
}