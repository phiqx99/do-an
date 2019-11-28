using BEQLDT.Data.Repositories;
using BEQLDT.Infrastructure.Extension;
using BEQLDT.Model;
using BEQLDT.Model.Request;
using BEQLDT.Model.Result;
using BEQLDT.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Claims;

namespace BEQLDT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SchoolController : ControllerBase
    {
        private readonly ISchoolRepository _schoolRepository;
        private readonly ISchoolService _schoolService;

        public SchoolController(ISchoolRepository schoolRepository, ISchoolService schoolService)
        {
            _schoolRepository = schoolRepository;
            _schoolService = schoolService;
        }

        #region Created School
        [HttpPost]
        [Route("Created")]
        public IActionResult Created(SchoolViewModel schoolVm)
        {
            var result = new ResultApi(true);
            if (!ModelState.IsValid)
            {
                result.ErrorCode = (int)ErrorCode.DataInvalid;
                result.Message = "Dữ liệu không hợp lệ !";
            }
            else if (_schoolRepository.CheckContains(n => n.NameSchool == schoolVm.NameSchool))
            {
                result.ErrorCode = (int)ErrorCode.NameAlreadyExists;
                result.Message = "Tên đã tồn tại !";
            }
            else
            {
                var schoolModel = new School();
                var claimIdUser = HttpContext.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
                int idUser = Convert.ToInt32(claimIdUser);
                schoolVm.Active = true;
                schoolVm.CreatedAt = DateTime.Now;
                schoolVm.CreatedUser = idUser;
                schoolVm.UpdateAt = schoolModel.UpdateAt;
                schoolVm.UpdateUser = schoolModel.UpdateUser;
                schoolModel.UpdateSchoolModel(schoolVm);
                _schoolService.Add(schoolModel);
                _schoolService.SaveChange();

                result.Success = true;
                result.Data = schoolModel;
            }
            return Ok(result);
        }
        #endregion

        #region Update
        [HttpPut]
        [Route("Update")]
        public IActionResult Update(SchoolViewModel schoolVm, int id)
        {
            var result = new ResultApi(true);
            var schoolModel = _schoolService.GetById(id);
            if (schoolModel == null)
            {
                result.ErrorCode = (int)ErrorCode.ObjectNotExits;
                result.Message = "School không tồn tại !";
            }
            else if (schoolModel.NameSchool != schoolVm.NameSchool)
            {
                result.ErrorCode = (int)ErrorCode.ObjectDontChange;
                result.Message = "Không được thay đổi tên School !";
            }
            else
            {
                var claimIdUser = HttpContext.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
                int idUser = Convert.ToInt32(claimIdUser);
                schoolVm.Active = true;
                schoolVm.UpdateAt = DateTime.Now;
                schoolVm.UpdateUser = idUser;
                schoolVm.CreatedAt = schoolModel.CreatedAt;
                schoolVm.CreatedUser = schoolModel.CreatedUser;
                schoolModel.UpdateSchoolModel(schoolVm);
                _schoolService.Update(schoolModel);
                _schoolService.SaveChange();

                result.Success = true;
                result.Data = schoolModel;
            }
            return Ok(result);
        }
        #endregion

        #region Delete
        [HttpDelete]
        [Route("Delete")]
        public IActionResult Delete(int id)
        {
            var result = new ResultApi(false);
            var checkId = _schoolService.CheckDelete(id);
            if (checkId == true)
            {
                result.Success = false;
                result.Message = "School đang được sử dụng";
                
            }
            else if(_schoolRepository.CheckContains(n=>n.Id==id))
            {
                _schoolService.Delete(id);
                _schoolService.SaveChange();

                result.Success = true;
                result.Message = "Xóa School thành công !";

            }
            else
            {
                result.ErrorCode = (int)ErrorCode.DeleteFailed;
                result.Message = "School không tồn tại !";
            }
            return Ok(result);
        }
        #endregion

        #region Get
        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetAll(int page = 1, int pageSize = 10)
        {
            var result = new ResultApi(true);
            var model = _schoolService.GetAllPage(page, pageSize);
            if (model != null)
            {
                result.Success = true;
                result.Data = model;
            }
            else
            {
                result.ErrorCode = (int)ErrorCode.ObjectNotExits;
                result.Message = "School rỗng !";
            }
            return Ok(result);
        }
        [HttpGet]
        [Route("GetById")]
        public IActionResult GetById(int id)
        {
            var result = new ResultApi(false);
            var model = _schoolService.GetById(id);
            if (model != null)
            {
                result.Success = true;
                result.Data = model;
            }
            else
            {
                result.ErrorCode = (int)ErrorCode.ObjectNotExits;
                result.Message = "SchoolId không tồn tại !";
            }
            return Ok(result);
        }
        #endregion

        #region Search School
        [HttpGet]
        [Route("Search")]
        public IActionResult SeachUsers(string searchString)
        {
            var result = new ResultApi(true);
            if (searchString != null)
            {
                var search = _schoolService.Search(searchString);
                result.Data = search;

            }
            else
            {
                result.ErrorCode = (int)ErrorCode.UserNotFound;
                result.Message = "Không tìm thấy đối tượng";
            }
            return Ok(result);

        }
        #endregion

    }
}