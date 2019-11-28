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
    public class DecisionController : ControllerBase
    {
        private readonly IDecisionRepository _decisionRepository;
        private readonly IDecisionService _decisionService;

        public DecisionController(IDecisionRepository decisionRepository, IDecisionService decisionService)
        {
            _decisionRepository = decisionRepository;
            _decisionService = decisionService;
        }


        #region Created Decision
        [HttpPost]
        [Route("Created")]
        public IActionResult Created(DecisionViewModel decisionVm)
        {
            var result = new ResultApi(true);
            if (!ModelState.IsValid)
            {
                result.ErrorCode = (int)ErrorCode.DataInvalid;
                result.Message = "Dữ liệu không hợp lệ !";
            }
            else if (_decisionRepository.CheckContains(n => n.NameDecision == decisionVm.NameDecision))
            {
                result.ErrorCode = (int)ErrorCode.NameAlreadyExists;
                result.Message = "Tên đã tồn tại !";
            }
            else
            {
                var decisionModel = new Decision();
                var claimIdUser = HttpContext.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
                int idUser = Convert.ToInt32(claimIdUser);
                decisionVm.Active = true;
                decisionVm.CreatedAt = DateTime.Now;
                decisionVm.CreatedUser = idUser;
                decisionVm.UpdateAt = decisionModel.UpdateAt;
                decisionVm.UpdateUser = decisionModel.UpdateUser;
                decisionModel.UpdateDecisionModel(decisionVm);
                _decisionService.Add(decisionModel);
                _decisionService.SaveChange();

                result.Success = true;
                result.Data = decisionModel;
            }
            return Ok(result);
        }
        #endregion

        #region Update
        [HttpPut]
        [Route("Update")]
        public IActionResult Update(DecisionViewModel decisionVm, int id)
        {
            var result = new ResultApi(true);
            var decisionModel = _decisionService.GetById(id);
            if (decisionModel == null)
            {
                result.ErrorCode = (int)ErrorCode.ObjectNotExits;
                result.Message = "Decision không tồn tại !";
            }
            else if (decisionModel.NameDecision != decisionVm.NameDecision)
            {
                result.ErrorCode = (int)ErrorCode.ObjectDontChange;
                result.Message = "Không được thay đổi tên Decision !";
            }
            else
            {
                var claimIdUser = HttpContext.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
                int idUser = Convert.ToInt32(claimIdUser);
                decisionVm.Active = true;
                decisionVm.UpdateAt = DateTime.Now;
                decisionVm.UpdateUser = idUser;
                decisionVm.CreatedAt = decisionModel.CreatedAt;
                decisionVm.CreatedUser = decisionModel.CreatedUser;
                decisionModel.UpdateDecisionModel(decisionVm);
                _decisionService.Update(decisionModel);
                _decisionService.SaveChange();

                result.Success = true;
                result.Data = decisionModel;
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
            var checkId = _decisionService.CheckDelete(id);
            if (checkId == true)
            {
                result.Success = false;
                result.Message = "Decision đang được sử dụng";
            }
            else if(_decisionRepository.CheckContains(n=>n.Id==id))
            {
                _decisionService.Delete(id);
                _decisionService.SaveChange();

                result.Success = true;
                result.Message = "Xóa Decision thành công !";

            }
            else
            {
                result.ErrorCode = (int)ErrorCode.DeleteFailed;
                result.Message = " Decision không tồn tại !";
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
            var model = _decisionService.GetAllPage(page, pageSize);
            if (model != null)
            {
                result.Success = true;
                result.Data = model;
            }
            else
            {
                result.ErrorCode = (int)ErrorCode.ObjectNotExits;
                result.Message = "Decision rỗng !";
            }
            return Ok(result);
        }
        [HttpGet]
        [Route("GetById")]
        public IActionResult GetById(int id)
        {
            var result = new ResultApi(false);
            var model = _decisionService.GetById(id);
            if (model != null)
            {
                result.Success = true;
                result.Data = model;
            }
            else
            {
                result.ErrorCode = (int)ErrorCode.ObjectNotExits;
                result.Message = "Decision không tồn tại !";
            }
            return Ok(result);
        }
        #endregion
    }
}