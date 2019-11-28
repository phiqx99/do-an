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
    public class PeriodController : ControllerBase
    {
        private readonly IPeriodRepository _periodRepository;
        private readonly IPeriodService _periodService;

        public PeriodController(IPeriodRepository periodRepository, IPeriodService periodService)
        {
            _periodRepository = periodRepository;
            _periodService = periodService;
        }

        #region Created Period
        [HttpPost]
        [Route("Created")]
        public IActionResult Created(PeriodViewModel periodVm)
        {
            var result = new ResultApi(true);
            if (!ModelState.IsValid)
            {
                result.ErrorCode = (int)ErrorCode.DataInvalid;
                result.Message = "Dữ liệu không hợp lệ !";
            }
            else if (_periodRepository.CheckContains(n => n.Caption == periodVm.Caption))
            {
                result.ErrorCode = (int)ErrorCode.NameAlreadyExists;
                result.Message = "Tên đã tồn tại !";
            }
            else
            {
                var periodModel = new Period();
                var claimIdUser = HttpContext.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
                int idUser = Convert.ToInt32(claimIdUser);
                periodVm.Active = true;
                periodVm.CreatedAt = DateTime.Now;
                periodVm.CreatedUser = idUser;
                periodVm.UpdateAt = periodModel.UpdateAt;
                periodVm.UpdateUser = periodModel.UpdateUser;
                periodModel.UpdatePeriodModel(periodVm);
                _periodService.Add(periodModel);
                _periodService.SaveChange();

                result.Success = true;
                result.Data = periodModel;
            }
            return Ok(result);
        }
        #endregion

        #region Update
        [HttpPut]
        [Route("Update")]
        public IActionResult Update(PeriodViewModel periodVm, int id)
        {
            var result = new ResultApi(true);
            var periodModel = _periodService.GetById(id);
            if (periodModel == null)
            {
                result.ErrorCode = (int)ErrorCode.ObjectNotExits;
                result.Message = "Period không tồn tại !";
            }
            else if (periodModel.Caption != periodVm.Caption)
            {
                result.ErrorCode = (int)ErrorCode.ObjectDontChange;
                result.Message = "Không được thay đổi tên Period !";
            }
            else
            {
                var claimIdUser = HttpContext.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
                int idUser = Convert.ToInt32(claimIdUser);
                periodVm.Active = true;
                periodVm.UpdateAt = DateTime.Now;
                periodVm.UpdateUser = idUser;
                periodVm.CreatedAt = periodModel.CreatedAt;
                periodVm.CreatedUser = periodModel.CreatedUser;
                periodModel.UpdatePeriodModel(periodVm);
                _periodService.Update(periodModel);
                _periodService.SaveChange();

                result.Success = true;
                result.Data = periodModel;
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
            var checkId = _periodService.CheckDelete(id);
            if (checkId == true)
            {
                result.Success = false;
                result.Message = "Period đang được sử dụng";

            }
            else if (_periodRepository.CheckContains(n => n.Id == id))
            {
                _periodService.Delete(id);
                _periodService.SaveChange();

                result.Success = true;
                result.Message = "Xóa Period thành công !";

            }
            else
            {
                result.ErrorCode = (int)ErrorCode.DeleteFailed;
                result.Message = " Period không tồn tại !";
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
            var model = _periodService.GetAllPage(page, pageSize);
            if (model != null)
            {
                result.Success = true;
                result.Data = model;
            }
            else
            {
                result.ErrorCode = (int)ErrorCode.ObjectNotExits;
                result.Message = "Period rỗng !";
            }
            return Ok(result);
        }
        [HttpGet]
        [Route("GetById")]
        public IActionResult GetById(int id)
        {
            var result = new ResultApi(false);
            var model = _periodService.GetById(id);
            if (model != null)
            {
                result.Success = true;
                result.Data = model;
            }
            else
            {
                result.ErrorCode = (int)ErrorCode.ObjectNotExits;
                result.Message = "Period không tồn tại !";
            }
            return Ok(result);
        }
        #endregion
    }
}