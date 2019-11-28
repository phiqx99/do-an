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
    public class CouncilController : ControllerBase
    {
        private readonly ICouncilRepository _councilRepository;
        private readonly ICouncilService _councilService;


        public CouncilController(ICouncilRepository councilRepository, ICouncilService councilService)
        {
            _councilRepository = councilRepository;
            _councilService = councilService;
        }

        #region Created Council
        [HttpPost]
        [Route("Created")]
        public IActionResult Created(CouncilViewModel councilVm)
        {
            var result = new ResultApi(true);
            if (!ModelState.IsValid)
            {
                result.ErrorCode = (int)ErrorCode.DataInvalid;
                result.Message = "Dữ liệu không hợp lệ !";
            }
            else if (_councilRepository.CheckContains(n => n.NameCouncil == councilVm.NameCouncil))
            {
                result.ErrorCode = (int)ErrorCode.NameAlreadyExists;
                result.Message = "Tên đã tồn tại !";
            }
            else
            {
                var councilModel = new Council();
                var claimIdUser = HttpContext.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
                int idUser = Convert.ToInt32(claimIdUser);
                councilVm.Active = true;
                councilVm.CreatedAt = DateTime.Now;
                councilVm.CreatedUser = idUser;
                councilVm.UpdateAt = councilModel.UpdateAt;
                councilVm.UpdateUser = councilModel.UpdateUser;
                councilModel.UpdateCouncilModel(councilVm);
                _councilService.Add(councilModel);
                _councilService.SaveChange();

                result.Success = true;
                result.Data = councilModel;
            }
            return Ok(result);
        }
        #endregion

        #region Update
        [HttpPut]
        [Route("Update")]
        public IActionResult Update(CouncilViewModel councilVm, int id)
        {
            var result = new ResultApi(true);
            var councilModel = _councilService.GetById(id);
            if (councilModel == null)
            {
                result.ErrorCode = (int)ErrorCode.ObjectNotExits;
                result.Message = "Hội đồng không tồn tại !";
            }
            else if (councilModel.NameCouncil != councilVm.NameCouncil)
            {
                result.ErrorCode = (int)ErrorCode.ObjectDontChange;
                result.Message = "Không được thay đổi tên hội đồng !";
            }
            else
            {
                var claimIdUser = HttpContext.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
                int idUser = Convert.ToInt32(claimIdUser);
                councilVm.Active = true;
                councilVm.UpdateAt = DateTime.Now;
                councilVm.UpdateUser = idUser;
                councilVm.CreatedAt = councilModel.CreatedAt;
                councilVm.CreatedUser = councilModel.CreatedUser;
                councilModel.UpdateCouncilModel(councilVm);
                _councilService.Update(councilModel);
                _councilService.SaveChange();

                result.Success = true;
                result.Data = councilModel;
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
            var checkId = _councilService.CheckDelete(id);
            if (checkId == true)
            {
                result.Success = false;
                result.Message = "Hội đồng đang được sử dụng";

            }
            else if(_councilRepository.CheckContains(n=>n.Id==id))
            {

                _councilService.Delete(id);
                _councilService.SaveChange();

                result.Success = true;
                result.Message = "Xóa Hội đồng thành công !";
            }
            else
            {
                result.ErrorCode = (int)ErrorCode.DeleteFailed;
                result.Message = " Hội đồng không tồn tại !";
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
            var model = _councilService.GetAllPage(page, pageSize);
            if (model != null)
            {
                result.Success = true;
                result.Data = model;
            }
            else
            {
                result.ErrorCode = (int)ErrorCode.ObjectNotExits;
                result.Message = "Council rỗng !";
            }
            return Ok(result);
        }
        [HttpGet]
        [Route("GetById")]
        public IActionResult GetById(int id)
        {
            var result = new ResultApi(false);
            var model = _councilService.GetById(id);
            if (model != null)
            {
                result.Success = true;
                result.Data = model;
            }
            else
            {
                result.ErrorCode = (int)ErrorCode.ObjectNotExits;
                result.Message = "CouncilId không tồn tại !";
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
                var model = _councilService.Searchs(searchString);
                result.Success = true;
                result.Data = model;
            }
            else
            {
                result.ErrorCode = (int)ErrorCode.ObjectNotExits;
                result.Message = "Không tìm thấy đối tượng";
            }
            return Ok(result);
        }
        #endregion

    }
}