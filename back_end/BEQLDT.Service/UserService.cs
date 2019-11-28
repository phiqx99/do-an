using BEQLDT.Data;
using BEQLDT.Data.Infrastructure;
using BEQLDT.Data.Repositories;
using BEQLDT.Model;
using BEQLDT.Service.KeycloakService;
using BEQLDT.Service.Model;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BEQLDT.Service
{
    public interface IUserService
    {
        User Add(User user);
        void Update(User user);
        User Delete(int id);
        IEnumerable<User> GetAll();
        IEnumerable<User> Search(string searchString);
        User GetById(int id);
        void SaveChange();
        string GetSSOSub();
        User LoginUser(string username, string password);
        IEnumerable<UserRoles> GetRoleUser(string userName);
        PaginationSet<User> GetAllPage(int page, int pageSize);
        bool GrantData(string type, object data = null);
        bool CheckDelete(int id);
        User GetUserBySSO();
    }
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly FDbContext db;
        

        private readonly Config _config;
        private readonly RestClient _client;
        private const string GrantCodeUrl = "/protocol/openid-connect/token";
        public GrantModel Grant;


        public UserService(IUserRepository userRepository, IUnitOfWork unitOfWork, Config config, FDbContext fDbContext
           )
        {
            this._userRepository = userRepository;
            db = fDbContext;
            this._unitOfWork = unitOfWork;
            _config = config;
            _client = new RestClient(config.RealmUrl);
        }


        public bool CheckDelete(int id)
        {
            var grU = db.GroupUsers.FirstOrDefault(n => n.UserId == id);
            if (grU == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public string GetSSOSub()
        {
            var ssoSub = Grant.AccessTokenModel.Payload.Sub;
            return ssoSub.ToString();
        }

        #region Login Thường
        public User LoginUser(string username , string password)
        {
         var model = _userRepository.GetSingleByCondition(n => n.Username == username && n.Password == password);
         return model;
        }
        #endregion

        #region LoginWithKeycloak

        public User GetUserBySSO()
        {
            var ssoSub = Grant.AccessTokenModel.Payload.Sub;
            return _userRepository.GetSingleByCondition(x => x.SSOSub == ssoSub);

        }
        private static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }
        public static string Base64Decode(string base64EncodedData)
        {
            base64EncodedData = base64EncodedData.Replace(" ", "+");
            int mod4 = base64EncodedData.Length % 4;
            if (mod4 > 0)
            {
                base64EncodedData += new string('=', 4 - mod4);
            }
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }
        public bool GrantData(string type, object data = null)
        {
            if (type == "code")
            {
                return data != null && GrantFromCode(data.ToString());
            }

            return false;

        }
        public bool GrantFromCode(string code, string redirectUri = "", string sessionHost = "")
        {
            var data = new GrantCodeModel()
            {
                client_id = _config.ClientId,
                //ApplicationSessionHost = sessionHost,
                code = code,
                grant_type = "authorization_code",
                redirect_uri = String.IsNullOrEmpty(redirectUri) ? _config.RedirectUrl : redirectUri
            };

            var request = new RestRequest(GrantCodeUrl, Method.POST);

            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");

            request.AddHeader("Authorization",
                "Basic " + Base64Encode(_config.ClientId + ":" + _config.Secret));
            request.AddObject(data);
            var response = _client.Execute(request);
            if ((int)response.StatusCode < 200 || (int)response.StatusCode > 299)
            {
                return false;
            }

            Grant = JsonConvert.DeserializeObject<GrantModel>(response.Content);
            return true;
        }

        #endregion

        public IEnumerable<User> Search(string searchString)
        {
            var getAll = _userRepository.GetAll();
            var model = from u in getAll
                        select new User
                        {
                            Id = u.Id,
                            Username = u.Username,
                            FullName = u.FullName,
                            Phone = u.Phone,
                            Email = u.Email
                        };
            if (!String.IsNullOrEmpty(searchString))
            {
                model = model.Where(s => s.FullName.ToLower().Contains(searchString)|| s.Username.ToLower().Contains(searchString)).Take(10);
            }
            return model.ToList();

        }

        public IEnumerable<UserRoles> GetRoleUser(string userName)
        {
            var model = from T1 in db.Users
                        join T2 in db.GroupUsers on T1.Id equals T2.UserId
                        join T3 in db.Groups on T2.GroupId equals T3.Id
                        join T4 in db.Permissions on T3.Id equals T4.GroupId
                        join T5 in db.Roles on T4.RoleId equals T5.Id
                        where T1.Username == userName
                        select new UserRoles
                        {
                            Id=T1.Id,
                            Username = T1.Username,
                            FullName = T1.FullName,
                            Email = T1.Email,
                            Gender = T1.Gender,
                            Phone = T1.Phone,
                            NameRole = T5.NameRole,
                            DateOfBirth = T1.DateOfBirth,
                            Address = T1.Address

                        };
            return model.ToList();
        }

        public PaginationSet<User> GetAllPage(int page, int pageSize)
        {
            var getall = _userRepository.GetAll();
            var results = from u in getall
                          select new User
                          {
                              Id = u.Id,
                              Username = u.Username,
                              FullName = u.FullName,
                              Gender = u.Gender,
                              DateOfBirth = u.DateOfBirth,
                              Phone = u.Phone,
                              Email = u.Email,
                          };
            var result = GetPagedResultForQuery(results, page, pageSize);
            return result;
        }
        private static PaginationSet<User> GetPagedResultForQuery(
       IEnumerable<User> query, int page, int pageSize)
        {
            var result = new PaginationSet<User>();
            result.PageNo = page;
            result.PageSize = pageSize;
            result.TotalCount = query.Count();
            var pageCount = (double)result.TotalCount / pageSize;
            result.Total = (int)Math.Ceiling(pageCount);
            var skip = (page - 1) * pageSize;
            result.Items = query.Skip(skip).Take(pageSize).ToList();
            return result;
        }

     
        public User Add(User user)
        {
            return _userRepository.Add(user);
        }

        public User Delete(int id)
        {
            return _userRepository.Delete(id);
        }

        public IEnumerable<User> GetAll()
        {
            return _userRepository.GetAll();
        }
        public User GetById(int id)
        {
            return _userRepository.GetSingleById(id);
        }

        public void SaveChange()
        {
            _unitOfWork.Commit();
        }

        public void Update(User user)
        {
            _userRepository.Update(user);
        }
    }
}
