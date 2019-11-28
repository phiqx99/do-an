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
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;

namespace BEQLDT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FileController : ControllerBase
    {
        private readonly IFileRepository _fileRepository;
        private readonly IFileService _fileService;

        public FileController(IFileRepository fileRepository, IFileService fileService)
        {
            _fileRepository = fileRepository;
            _fileService = fileService;
        }

        #region Created File
        [HttpPost]
        [Route("Created")]
        public IActionResult Created(FileViewModel fileVm)
        {
            var result = new ResultApi(true);
            if (!ModelState.IsValid)
            {
                result.ErrorCode = (int)ErrorCode.DataInvalid;
                result.Message = "Dữ liệu không hợp lệ !";
            }
            else if (_fileRepository.CheckContains(n => n.NameFile == fileVm.NameFile))
            {
                result.ErrorCode = (int)ErrorCode.NameAlreadyExists;
                result.Message = "Tên đã tồn tại !";
            }
            else
            {
                var fileModel = new Filed();
   
                var claimIdUser = HttpContext.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
                int idUser = Convert.ToInt32(claimIdUser);
                fileVm.Active = true;
                fileVm.CreatedAt = DateTime.Now;
                fileVm.CreatedUser = idUser;
                fileVm.UpdateAt = fileModel.UpdateAt;
                fileVm.UpdateUser = fileModel.UpdateUser;
                //var file = Request.Form.Files[0];
                //var folderName = Path.Combine("Files");
                //var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                //if(file.Length > 0)
                //{
                //    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                //    var fullPath = Path.Combine(pathToSave, fileName);
                //    var dbPath = Path.Combine(folderName, fileName);

                //    using (var stream = new FileStream(fullPath, FileMode.Create))
                //    {
                //        file.CopyTo(stream);
                //    }
                //}
                fileModel.UpdateFileModel(fileVm);
                _fileService.Add(fileModel);
                _fileService.SaveChange();

                result.Success = true;
                result.Data = fileModel;
            }
            return Ok(result);
        }
        #endregion

        #region Update
        [HttpPut]
        [Route("Update")]
        public IActionResult Update(FileViewModel fileVm, int id)
        {
            var result = new ResultApi(true);
            var fileModel = _fileService.GetById(id);
            if (fileModel == null)
            {
                result.ErrorCode = (int)ErrorCode.ObjectNotExits;
                result.Message = "File không tồn tại !";
            }
            else if (fileModel.NameFile != fileVm.NameFile)
            {
                result.ErrorCode = (int)ErrorCode.ObjectDontChange;
                result.Message = "Không được thay đổi tên File !";
            }
            else
            {
                var claimIdUser = HttpContext.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
                int idUser = Convert.ToInt32(claimIdUser);
                fileVm.Active = true;
                fileVm.UpdateAt = DateTime.Now;
                fileVm.UpdateUser = idUser;
                fileVm.CreatedAt = fileModel.CreatedAt;
                fileVm.CreatedUser = fileModel.CreatedUser;
                fileModel.UpdateFileModel(fileVm);
                _fileService.Update(fileModel);
                _fileService.SaveChange();

                result.Success = true;
                result.Data = fileModel;
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
            if(_fileRepository.CheckContains(n=>n.Id==id))
            {
                _fileService.Delete(id);
                _fileService.SaveChange();

                result.Success = true;
                result.Message = "Xóa File thành công !";

            }
            else
            {
                result.ErrorCode = (int)ErrorCode.DeleteFailed;
                result.Message = " File không tồn tại !";
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
            var model = _fileService.GetAllPage(page, pageSize);
            if (model != null)
            {
                result.Success = true;
                result.Data = model;
            }
            else
            {
                result.ErrorCode = (int)ErrorCode.ObjectNotExits;
                result.Message = "File rỗng !";
            }
            return Ok(result);
        }
        [HttpGet]
        [Route("GetById")]
        public IActionResult GetById(int id)
        {
            var result = new ResultApi(false);
            var model = _fileService.GetById(id);
            if (model != null)
            {
                result.Success = true;
                result.Data = model;
            }
            else
            {
                result.ErrorCode = (int)ErrorCode.ObjectNotExits;
                result.Message = "File không tồn tại !";
            }
            return Ok(result);
        }
        #endregion

    }
}