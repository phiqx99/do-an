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
    public class TopicController : ControllerBase
    {
        private readonly ITopicRepository _topicRepository;
        private readonly ITopicService _topicService;
        private readonly IFileService _fileService;


        public TopicController(ITopicRepository topicRepository, ITopicService topicService, IFileService fileService)
        {
            _topicRepository = topicRepository;
            _topicService = topicService;
            _fileService = fileService;
        }

        #region Created Topic All
        [HttpPost]
        [Route("Created")]
        public IActionResult Created(TopicViewModel topicVm)
        {
            var result = new ResultApi(true);
            if (!ModelState.IsValid)
            {
                result.ErrorCode = (int)ErrorCode.DataInvalid;
                result.Message = "Dữ liệu không hợp lệ";
            }
            else
            {
                var topicModel = new Topic();
                var nameFile = topicVm.NameFile.Substring(12);
                var claimIdUser = HttpContext.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
                int idUser = Convert.ToInt32(claimIdUser);
                topicVm.Active = true;
                topicVm.CreatedAt = DateTime.Now;
                topicVm.CreatedUser = idUser;
                topicVm.UpdateAt = topicModel.UpdateAt;
                topicVm.UpdateUser = topicModel.UpdateUser;
                topicModel.UpdateTopicModel(topicVm);
                _topicService.Add(topicModel);
                _topicService.SaveChange();
                if (topicModel.Id != 0)
                {
                    var fileModel = new Filed();
                    var fileVm = new FileViewModel();
                    fileVm.TopicId = topicModel.Id;
                    fileVm.NameFile = nameFile;
                    //fileVm.Base64File = topicVm.Base64File;
                    fileModel.UpdateFileModel(fileVm);
                    _fileService.Add(fileModel);
                    _fileService.SaveChange();
                }
                var rs = _topicService.GetById(topicModel.Id);
                //var stream = System.IO.File.OpenRead(@"c:\path\to\your\file\here.txt");
                //byte[] fileBytes = System.IO.File.ReadAllBytes(topicVm.NameFile);
                string base64 = topicVm.Base64File.Split(',')[1];
                var decodedFileBytes = Convert.FromBase64String(base64);
                System.IO.File.WriteAllBytes("Files/" + nameFile, decodedFileBytes);

                result.Success = true;
                result.Data = rs;
            }
            return Ok(result);
        }
        #endregion

        #region Update
        [HttpPut]
        [Route("Update")]
        public IActionResult Update(TopicViewModel topicVm, int id)
        {
            var result = new ResultApi(true);
            var topicModel = _topicService.GetById(id);
            if (topicModel == null)
            {
                result.ErrorCode = (int)ErrorCode.ObjectNotExits;
                result.Message = "Đối tượng không tồn tại";
            }
            else
            {
                if (topicVm.PeriodId != null)
                {
                    _topicService.UpdatePeriodId(topicVm.PeriodId, id);

                }
                else if (topicVm.PeriodId == null)
                {
                    _topicService.UpdatePeriodId(topicVm.PeriodId, id);
                }
                else
                {
                    var claimIdUser = HttpContext.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
                    int idUser = Convert.ToInt32(claimIdUser);
                    topicVm.Active = true;
                    topicVm.UpdateAt = DateTime.Now;
                    topicVm.UpdateUser = idUser;
                    topicVm.CreatedAt = topicModel.CreatedAt;
                    topicVm.CreatedUser = topicModel.CreatedUser;
                    topicModel.UpdateTopicModel(topicVm);
                    _topicService.Update(topicModel);
                }
                _topicService.SaveChange();

                result.Success = true;
                result.Data = topicModel;
            }
            return Ok(result);
        }
        [HttpPut]
        [Route("PassTopic")]
        public IActionResult PassTopic(TopicViewModel topicVm, int id)
        {
            var result = new ResultApi(true);
            var periodModel = _topicService.GetById(id);
            if (periodModel == null)
            {
                result.ErrorCode = (int)ErrorCode.ObjectNotExits;
                result.Message = "Period không tồn tại !";
            }
            else
            {
                _topicService.PassTopic(topicVm.PeriodId, id);
                _topicService.SaveChange();

                result.Success = true;
                result.Data = periodModel;
            }
            return Ok(result);
        }
        #endregion

        #region Delete
        [HttpDelete]
        [Route("Delete")]
        //[Authorize(Roles = "user")]
        public IActionResult Delete(int id)
        {
            var result = new ResultApi(true);
            var model = _topicService.GetById(id);
            if (model != null)
            {
                _topicService.Delete(id);
                _topicService.SaveChange();
                result.Success = true;
                result.Message = "Xóa thành công";
            }
            else
            {
                result.ErrorCode = (int)ErrorCode.ObjectNotExits;
                result.Message = "Đối tượng không tồn tại";
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
            var model = _topicService.GetAllPage(page, pageSize);
            if (model != null)
            {
                result.Success = true;
                result.Data = model;
            }
            else
            {
                result.ErrorCode = (int)ErrorCode.ObjectNotExits;
                result.Message = "Topic rỗng !";
            }
            return Ok(result);
        }
        [HttpGet]
        [Route("GetById")]
        public IActionResult GetById(int id)
        {
            var result = new ResultApi(false);
            var model = _topicService.GetById(id);
            if (model != null)
            {
                result.Success = true;
                result.Data = model;
            }
            else
            {
                result.ErrorCode = (int)ErrorCode.ObjectNotExits;
                result.Message = "Topic không tồn tại !";
            }
            return Ok(result);
        }
        [HttpGet]
        [Route("GetEditById")]
        public IActionResult GetEditById(int id)
        {
            var result = new ResultApi(false);
            var model = _topicService.GetEditById(id);
            if (model != null)
            {
                result.Success = true;
                result.Data = model;
            }
            else
            {
                result.ErrorCode = (int)ErrorCode.ObjectNotExits;
                result.Message = "Topic không tồn tại !";
            }
            return Ok(result);
        }
        [HttpGet]
        [Route("GetTopicByPeriodId")]
        public IActionResult GetTopicByPeriodId(int id, int page = 1, int pageSize = 10)
        {
            var result = new ResultApi(false);
            var model = _topicService.GetByIdPeriod(id, page, pageSize);
            if (model != null)
            {
                result.Success = true;
                result.Data = model;
            }
            else
            {
                result.ErrorCode = (int)ErrorCode.ObjectNotExits;
                result.Message = "Đối tượng không tồn tại !";
            }
            return Ok(result);
        }
        [HttpGet]
        [Route("GetTopicBySchoolId")]
        public IActionResult GetTopicBySchoolId(int id, int page = 1, int pageSize = 10)
        {
            var result = new ResultApi(false);
            var model = _topicService.GetByIdSchool(id, page, pageSize);
            if (model != null)
            {
                result.Success = true;
                result.Data = model;
            }
            else
            {
                result.ErrorCode = (int)ErrorCode.ObjectNotExits;
                result.Message = "Đối tượng không tồn tại !";
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
            if (searchString != null)
            {
                var model = _topicService.Searchs(searchString);
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