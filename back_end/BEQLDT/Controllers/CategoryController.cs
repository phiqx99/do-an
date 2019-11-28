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
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryeService;
        private readonly ICategoryRepository _categoryeRepository;

        public CategoryController(ICategoryRepository categoryeRepository, ICategoryService categoryeService)
        {
            _categoryeRepository = categoryeRepository;
            _categoryeService = categoryeService;
        }

        #region Created Category 
        [HttpPost]
        [Route("Created")]
        public IActionResult Created(CategoryViewModel categoryeVm)
        {
            var result = new ResultApi(true);
            if (!ModelState.IsValid)
            {
                result.ErrorCode = (int)ErrorCode.DataInvalid;
                result.Message = "Dữ liệu không hợp lệ !";
            }
            if (_categoryeRepository.CheckContains(n => n.NameCategory == categoryeVm.NameCategory))
            {
                result.ErrorCode = (int)ErrorCode.NameAlreadyExists;
                result.Message = "Tên chủ đề đã tồn tại !";
            }
            else
            {
                var categoryeModel = new Category();
                var claimsUser = HttpContext.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
                int idUser = Convert.ToInt32(claimsUser);
                categoryeVm.Active = true;
                categoryeVm.CreatedAt = DateTime.Now;
                categoryeVm.CreatedUser = idUser;
                categoryeVm.UpdateAt = categoryeModel.UpdateAt;
                categoryeVm.UpdateUser = categoryeModel.UpdateUser;

                categoryeModel.UpdateCategoryModel(categoryeVm);
                _categoryeService.Add(categoryeModel);
                _categoryeService.SaveChange();

                result.Success = true;
                result.Data = categoryeModel;
            }
            return Ok(result);
        }
        #endregion

        #region Update Category
        [HttpPut]
        [Route("Update")]
        public IActionResult Update(CategoryViewModel categoryeVm, int id)
        {
            var result = new ResultApi(true);
            var categoryeModel = _categoryeService.GetById(id);
            if (categoryeModel == null)
            {
                result.ErrorCode = (int)ErrorCode.ObjectNotExits;
                result.Message = "Chủ đề không tồn tại !";
            }
            else if(categoryeModel.NameCategory != categoryeVm.NameCategory)
            {
                result.ErrorCode = (int)ErrorCode.ObjectDontChange;
                result.Message = "Không thể thay đổi tên chủ đề ";
            }
            else
            {
                var claimsUser = HttpContext.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
                int idUser = Convert.ToInt32(claimsUser);
                categoryeVm.Active = categoryeModel.Active;
                categoryeVm.CreatedAt = categoryeModel.CreatedAt;
                categoryeVm.CreatedUser = categoryeModel.CreatedUser;
                categoryeVm.UpdateAt = DateTime.Now;
                categoryeVm.UpdateUser = idUser;
                categoryeModel.UpdateCategoryModel(categoryeVm);
                _categoryeService.Update(categoryeModel);
                _categoryeService.SaveChange();

                result.Success = true;
                result.Data = categoryeModel;
            }
            return Ok(result);
        }
        #endregion

        #region Delete Category
        [HttpDelete]
        [Route("Delete")]
        public IActionResult Delete(int id)
        {
            var result = new ResultApi(false);
            var checkId = _categoryeService.CheckDelete(id);
            if (checkId == true)
            {
                result.Success = false;
                result.Message = "chủ đề đang được sử dụng";
            }
            else if (_categoryeRepository.CheckContains(n => n.Id == id))
            {
                _categoryeService.Delete(id);
                _categoryeService.SaveChange();

                result.Success = true;
                result.Message = "Xóa chủ đề thành công !";
            }
            else
            {
                result.ErrorCode = (int)ErrorCode.DeleteFailed;
                result.Message = "chủ đề không tồn tại !";
            }
            return Ok(result);
        }
        #endregion

        #region Get Category
        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetAll(int page = 1 ,int pageSize = 10)
        {
            var model = _categoryeService.GetAllPage(page, pageSize);
            var result = new ResultApi(false);
            if (model != null)
            {

                result.Success = true;
                result.Data = model;
               
            }
            else
            {
                result.ErrorCode = (int)ErrorCode.ObjectNotExits;
                result.Message = "Không có chủ đề !";
            }
            return Ok(result);
        }

        [HttpGet]
        [Route("GetById")]
        public IActionResult GetById(int id)
        {
            var result = new ResultApi(true);
            var model = _categoryeService.GetById(id);
            if (model != null)
            {
                result.Success = true;
                result.Data = model;
            }
            else
            {
                result.ErrorCode = (int)ErrorCode.ObjectNotExits;
                result.Message = "Không có chủ đề !";
            }
            return Ok(result);
        }

        [HttpGet]
        [Route("GetTopicByCategoryId")]
        public IActionResult GetTopicByCategoryId(int id, int page = 1, int pageSize = 10)
        {
            var result = new ResultApi(true);
            var model = _categoryeService.GetTopicByCategoryId(id, page, pageSize);
            if (model != null)
            {
                result.Success = true;
                result.Data = model;
            }
            else
            {
                result.ErrorCode = (int)ErrorCode.ObjectNotExits;
                result.Message = "Không có đề tài !";
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
                var model = _categoryeService.Searchs(searchString);
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