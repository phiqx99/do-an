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
    public class TopicCouncilController : ControllerBase
    {
        private readonly ITopicCouncilRepository _topicCouncilRepository;
        private readonly ITopicCouncilService _topicCouncilService;

        public TopicCouncilController(ITopicCouncilRepository topicCouncilRepository, ITopicCouncilService topicCouncilService)
        {
            _topicCouncilRepository = topicCouncilRepository;
            _topicCouncilService = topicCouncilService;
        }

        #region Created ThemeCouncil
        [HttpPost]
        [Route("created")]
        public IActionResult Created(TopicCouncilViewModel topicCouncilVm)
        {
            var result = new ResultApi(true);
            var topicCouncilModel = new TopicCouncil();
            if (!ModelState.IsValid)
            {
                result.Success = false;
                result.ErrorCode = (int)ErrorCode.DataInvalid;
                result.Message = "Dữ liệu không hợp lệ !";
            }
            if (_topicCouncilRepository.CheckContains(n => n.TopicId == topicCouncilVm.TopicId
                 && n.CouncilId == topicCouncilVm.CouncilId))
            {
                result.Success = false;
                result.ErrorCode = (int)ErrorCode.ObjectExist;
                result.Message = "Đã tồn tại";
            }
            else
            {
                var claimIdUser = HttpContext.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
                int idUser = Convert.ToInt32(claimIdUser);
                topicCouncilVm.Active = true;
                topicCouncilVm.CreatedAt = DateTime.Now;
                topicCouncilVm.CreatedUser = idUser;
                topicCouncilVm.UpdateAt = topicCouncilModel.UpdateAt;
                topicCouncilVm.UpdateUser = topicCouncilModel.UpdateUser;
                topicCouncilModel.UpdateTopicCouncilModel(topicCouncilVm);
                _topicCouncilService.Add(topicCouncilModel);
                _topicCouncilService.SaveChange();

                result.Success = true;
                result.Data = topicCouncilModel;
            }

            return Ok(result);
        }
        #endregion

        #region Update
        [HttpPut]
        [Route("Update")]
        public IActionResult Update(TopicCouncilViewModel topicCouncilVm, int id)
        {
            var result = new ResultApi(true);
            var topicCouncilModel = _topicCouncilService.GetById(id);
            if (topicCouncilModel == null)
            {
                result.ErrorCode = (int)ErrorCode.ObjectNotExits;
                result.Message = "TopicCouncil không tồn tại !";
            }
            var claimIdUser = HttpContext.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            int idUser = Convert.ToInt32(claimIdUser);
            topicCouncilVm.Active = true;
            topicCouncilVm.UpdateAt = DateTime.Now;
            topicCouncilVm.UpdateUser = idUser;
            topicCouncilVm.CreatedAt = topicCouncilModel.CreatedAt;
            topicCouncilVm.CreatedUser = topicCouncilModel.CreatedUser;
            topicCouncilModel.UpdateTopicCouncilModel(topicCouncilVm);
            _topicCouncilService.Update(topicCouncilModel);
            _topicCouncilService.SaveChange();

            result.Success = true;
            result.Data = topicCouncilModel;

            return Ok(result);

        }
        #endregion

        #region Delete
        [HttpDelete]
        [Route("Delete")]
        public IActionResult Delete(int id)
        {
            var result = new ResultApi(true);
            var model = _topicCouncilService.GetById(id);
            if (model != null)
            {
                _topicCouncilService.Delete(id);
                _topicCouncilService.SaveChange();
                result.Success = true;
                result.Message = "Xóa thành công !";
            }
            else
            {
                result.ErrorCode = (int)ErrorCode.DeleteFailed;
                result.Message = "Xóa không thành công !";
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
            var model = _topicCouncilService.GetAllPage(page, pageSize);
            if (model != null)
            {
                result.Success = true;
                result.Data = model;
            }
            else
            {
                result.ErrorCode = (int)ErrorCode.ObjectNotExits;
                result.Message = "TopicCouncil rỗng !";
            }
            return Ok(result);
        }
        [HttpGet]
        [Route("GetByid")]
        public IActionResult GetByIdTopicCouncil(int id)
        {
            var result = new ResultApi(true);
            var model = _topicCouncilService.GetById(id);
            if (model != null)
            {
                result.Success = true;
                result.Data = model;
            }
            else
            {
                result.ErrorCode = (int)ErrorCode.ObjectNotExits;
                result.Message = "TopicCouncil rỗng !";
            }
            return Ok(result);
        }

        [HttpGet]
        [Route("GetCouncilByTopicId")]
        public IActionResult GetIdCouncil(int id, int page = 1, int pageSize = 10)
        {
            var result = new ResultApi(false);
            var model = _topicCouncilService.GetByIdCouncil(id, page, pageSize);
            if (model != null)
            {
                result.Success = true;
                result.Data = model;
            }
            else
            {
                result.ErrorCode = (int)ErrorCode.ObjectNotExits;
                result.Message = "TopicCouncil rỗng !";
            }
            return Ok(result);
        }

        #endregion
    }
}