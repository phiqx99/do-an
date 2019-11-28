using BEQLDT.Data.Repositories;
using BEQLDT.Infrastructure.Extension;
using BEQLDT.Model;
using BEQLDT.Model.Request;
using BEQLDT.Model.Result;
using BEQLDT.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;


namespace BEQLDT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    //[EnableCors("CorsPolicy")]
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        private readonly IUserRepository _userRepository;

        public AccountController(IUserService userService, IUserRepository userRepository) : base()
        {
            this._userService = userService;
            this._userRepository = userRepository;

        }

        #region Login User

        #region Login KeyCloak
        [HttpGet]
        [Route("loginKeyCloak")]
        [AllowAnonymous]
        public IActionResult LoginKeyCloak()
        {
            var result = new ResultApi(true);
            var code = HttpContext.Request.Query["code"];
            var state = HttpContext.Request.Query["state"];
            if (_userService.GrantData("code", code))
            {
                var model = _userService.GetUserBySSO();
                if (model != null)
                {
                    var authClaims = new[]
                    {
                    new Claim(ClaimTypes.Name, model.Username),
                    new Claim(ClaimTypes.NameIdentifier, model.Id.ToString()),

                    };
                    var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("mysite_supersecret_secretkeymysite_supersecret_secretkeymysite_supersecret_secretkeymysite_supersecret_secretkeymysite_supersecret_secretkeymysite_supersecret_secretkey"));

                    var token = new JwtSecurityToken(
                        issuer: "https://localhost:44346",
                        audience: "https://localhost:44346",
                        expires: DateTime.Now.AddHours(3),
                        claims: authClaims,
                        signingCredentials: new Microsoft.IdentityModel.Tokens.SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256Signature)
                        );


                    result.Data = new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(token),
                        expiration = token.ValidTo
                    };

                }
                else
                {
                    var ssoSub = _userService.GetSSOSub();
                    result.Message = "vui Lòng login bằng tài khoản thường để liên kết";
                    result.Data = ssoSub;
                }

                return Ok(result);
            }

            return Unauthorized();
        }
        #endregion

        #region Login Thường
        [HttpPost]
        [Route("Login")]
        [AllowAnonymous]
        public IActionResult Login(UserLogin userLogin)
        {
            var model = _userService.LoginUser(userLogin.UserName, userLogin.Password);
            var role = _userService.GetRoleUser(userLogin.UserName);
            if (userLogin.SSOSub != null && model != null)
            {
                model.SSOSub = userLogin.SSOSub;
                _userService.Update(model);
                _userService.SaveChange();

            }
            var result = new ResultApi(true);

            if (model != null)
            {
                var authClaims = new[]
                {
                    new Claim(ClaimTypes.Name, model.Username),
                    new Claim(ClaimTypes.NameIdentifier, model.Id.ToString()),

                    };
                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("mysite_supersecret_secretkeymysite_supersecret_secretkeymysite_supersecret_secretkeymysite_supersecret_secretkeymysite_supersecret_secretkeymysite_supersecret_secretkey"));

                var token = new JwtSecurityToken(
                    issuer: "https://localhost:44346",
                    audience: "https://localhost:44346",
                    expires: DateTime.Now.AddHours(3),
                    claims: authClaims,
                    signingCredentials: new Microsoft.IdentityModel.Tokens.SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256Signature)
                    );
                result.Data = new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = DateTime.Now.ToString("MM/dd/yyyy hh:mm tt")
                };


                return Ok(result);
            }
            return Unauthorized();
        }
        #endregion

        #endregion

        #region Update User
        [HttpPut]
        [Route("Update")]
        public IActionResult UpdateUser(UserViewModel userVM, int id)
        {
            var result = new ResultApi(false);
            var userModel = _userService.GetById(id);
            if (userModel == null)
            {
                result.ErrorCode = (int)ErrorCode.UserNotFound;
                result.Message = "Người dùng không tồn tại";
            }
            if (userVM.Password == null)
            {
                userVM.Password = userModel.Password;
            }

            var claimIdUser = HttpContext.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            int idUser = Convert.ToInt32(claimIdUser);
            userVM.Username = userModel.Username;
            userVM.Active = true;
            userVM.UpdateAt = DateTime.Now;
            userVM.UpdateUser = idUser;
            userVM.CreatedAt = userModel.CreatedAt;
            userVM.CreatedUser = userModel.CreatedUser;
            userModel.UpdateUserModel(userVM);
            _userService.Update(userModel);
            _userService.SaveChange();

            result.Success = true;
            result.Data = userModel;

            return Ok(result);
        }

        #endregion

        #region Register User
        [HttpPost]
        [Route("Register")]
        public IActionResult RegisterUser(UserViewModel userVM)
        {
            var result = new ResultApi(false);
            var userModel = new User();
            if (!ModelState.IsValid)
            {
                result.ErrorCode = (int)ErrorCode.DataInvalid;
                result.Message = "Dữ liệu không hợp lệ";
            }
            else if (userVM.Username == userModel.Username && userVM.Phone == userModel.Phone && userVM.Email == userModel.Email)
            {
                result.ErrorCode = (int)ErrorCode.ObjectExist;
                result.Message = "Username, Phone, Email đã tồn tại !";
            }
            else
            {
                var claimIdUser = HttpContext.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
                int idUser = Convert.ToInt32(claimIdUser);
                userVM.Active = true;
                userVM.CreatedAt = DateTime.Now;
                userVM.CreatedUser = idUser;
                userVM.UpdateAt = userModel.UpdateAt;
                userVM.UpdateUser = userModel.UpdateUser;
                userModel.UpdateUserModel(userVM);
                _userService.Add(userModel);
                _userService.SaveChange();

                result.Success = true;
                result.Data = userModel;
            }
            return Ok(result);
        }

        #endregion

        #region Get User
        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetAllUser(int page = 1, int pageSize = 10)
        {

            var model = _userService.GetAllPage(page, pageSize);
            var result = new ResultApi(false);
            if (model != null)
            {

                result.Success = true;
                result.Data = new
                {
                    model
                };
            }
            else
            {
                result.ErrorCode = (int)ErrorCode.ThereAreNoUsers;
                result.Message = "Người dùng rỗng";
            }
            return Ok(result);
        }
        [HttpGet]
        [Route("GetById")]
        public IActionResult GetId(int id)
        {
            var user = _userService.GetById(id);
            var result = new ResultApi(false);
            if (user != null)
            {
                result.Success = true;
                result.Data = user;
            }
            else
            {
                result.ErrorCode = (int)ErrorCode.UserNotFound;
                result.Message = "Người dùng rỗng";
            }
            return Ok(result);
        }
        [HttpGet]
        [Route("getinfor")]
        public IActionResult GetInfor()
        {
            var userName = HttpContext.User.Claims.First(c => c.Type == ClaimTypes.Name).Value;
            var role = _userService.GetRoleUser(userName);


            var result = new ResultApi(false);
            if (role != null)
            {
                result.Success = true;
                result.Data = role;
            }
            else
            {
                result.ErrorCode = (int)ErrorCode.UserNotFound;
                result.Message = "Người dùng rỗng";
            }
            return Ok(result);
        }
        #endregion

        #region Delete User
        [HttpDelete]
        [Route("Delete")]
        [AllowAnonymous]
        public IActionResult DeleteUser(int id)
        {
            var result = new ResultApi(false);
            var checkDel = _userService.CheckDelete(id);
            if (checkDel==true)
            {
                result.Success = false;
                result.Message = "Người dùng đang được sử dụng";
            }
            else if (_userRepository.CheckContains(n => n.Id == id))
            {
                _userService.Delete(id);
                _userService.SaveChange();
                result.Success = true;
                result.Message = "Xóa thành công";
            }
            else
            {
                result.ErrorCode = (int)ErrorCode.DeleteFailed;
                result.Message = "Người dùng không tồn tại";
            }
            return Ok(result);

        }
        #endregion

        #region Search User
        [HttpGet]
        [Route("Search")]
        public IActionResult SeachUsers(string searchString)
        {
            var result = new ResultApi(true);
            if (searchString != null)
            {
                var search = _userService.Search(searchString);
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

        #region Check
        [HttpGet]
        [Route("Check")]
        [AllowAnonymous]
        public IActionResult CheckValid(string check)
        {
            var result = new ResultApi(false);
            var user = _userRepository.GetAll();
            var checkUser = user.FirstOrDefault(n => n.Username == check);
            var checkEmail = user.FirstOrDefault(n => n.Email == check);
            var checkPhone = user.FirstOrDefault(n => n.Phone == check);
            if (checkUser != null)
            {
                result.Success = true;
                result.Data = checkUser;

            }
            else if(checkEmail != null)
            {
                result.Success = true;
                result.Data = checkEmail;
            }
            else if (checkPhone != null)
            {
                
                    result.Success = true;
                    result.Data = checkPhone;
                
            }
                
          
            return Ok(result);
}

        #endregion

    }
}