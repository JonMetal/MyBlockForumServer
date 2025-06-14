using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyBlockForumServer.Auth;
using MyBlockForumServer.DataBase.Entities;
using MyBlockForumServer.DataBase.Services;
using MyBlockForumServer.Tools;
using Thread = MyBlockForumServer.DataBase.Entities.Thread;

namespace MyBlockForumServer.Controllers
{

    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class UsersController : Controller
    {
        IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("{id}")]
        public ActionResult<User> GetUser(int id)
        {
            if (!RequestValidator.IdValidate(id))
            {
                return BadRequest();
            }
            return Ok(_userService.GetUser(id));
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public ActionResult<int> Login([FromBody] LoginRequestDetails loginRequestDetails)
        {
            try
            {
                var token = _userService.Login(loginRequestDetails.Login, loginRequestDetails.Password);

                CookieOptions cookieOptions = new CookieOptions
                {
                    HttpOnly = false,
                    Secure = false,
                };
                string id = _userService.GetUserByLogin(loginRequestDetails.Login)?.Id.ToString() ?? "";
                HttpContext.Response.Cookies.Append("snezhok_cookie", token, cookieOptions);
                HttpContext.Response.Cookies.Append("user_cookie", id);
                return Ok(token);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet]
        public IEnumerable<User> GetUsers()
        {
            return _userService.GetUsers();
        }

        [HttpGet("GetIdByLogin")]
        public ActionResult<int> GetByLogin(string Login)
        {
            return Ok(_userService.GetUserByLogin(Login)?.Id);
        }

        [HttpGet("AllStatuses")]
        [AllowAnonymous]
        public IEnumerable<Status> GetAllStatuses()
        {
            return _userService.GetAllStatuses();
        }

        [HttpGet("AllRoles")]
        public IEnumerable<Role> GetAllRoles()
        {
            return _userService.GetAllRoles();
        }

        [HttpGet("UsersFromThread/{id}")]
        public IEnumerable<User?> GetUsersFromThread(int id)
        {
            return _userService.GetUsersFromThread(id);
        }

        [HttpGet("UserRole/{id}")]
        public ActionResult<Role> GetUserRole(int id)
        {
            if (!RequestValidator.IdValidate(id))
            {
                return BadRequest();
            }
            return Ok(_userService.GetUserRole(id));
        }

        [HttpGet("UserThreads/{id}")]
        public ActionResult<Thread> GetUserThreads(int id)
        {
            if (!RequestValidator.IdValidate(id))
            {
                return BadRequest("GovnoId");
            }
            return Ok(_userService.GetUserThreads(id));
        }

        [HttpGet("UserStatus/{id}")]
        public ActionResult GetUserStatus(int id)
        {
            if (!RequestValidator.IdValidate(id))
            {
                return BadRequest();
            }
            return Ok(_userService.GetUserStatus(id));
        }

        [HttpPost("UserThread/{userId}/{threadId}")]
        public ActionResult AddUserThread(int userId, int threadId)
        {
            if (!RequestValidator.IdValidate(userId) || !RequestValidator.IdValidate(threadId))
            {
                return BadRequest("null id");
            }
            if (_userService.GetUserThreads(userId).FirstOrDefault(l => l?.Id == threadId) == null)
            {
                _userService.AddThreadUser(userId, threadId);
            }
            else
            {
                return BadRequest("User already is joined to thread");
            }
            return Ok();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult<User> CreateUser(User user)
        {
            try
            {
                if (!RequestValidator.ObjectValidate(user) || _userService.GetUsers().FirstOrDefault(l => l.Login == user.Login) != null)
                {
                    return BadRequest();
                }
                _userService.CreateUser(user);
                return Ok(user);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut]
        public ActionResult<User> SetUser(User user)
        {
            if (!RequestValidator.ObjectValidate(user))
            {
                return BadRequest();
            }
            _userService.SetUser(user);
            return Ok(user);
        }

        [HttpPut("UpdateStatus/{userId}/{statusId}")]
        public ActionResult<User> SetStatus(int userId, int statusId)
        {
            if (!RequestValidator.IdValidate(userId) || !RequestValidator.IdValidate(statusId))
            {
                return BadRequest("Id is not validated");
            }
            User? user = _userService.GetUser(userId);
            if (user == null)
            {
                return NotFound("User is null");
            }
            else
            {
                user.StatusId = statusId;
                _userService.SetUser(user);
                return Ok(user);
            }
        }


        [HttpDelete("{id}")]
        public ActionResult DeleteUser(int id)
        {
            if (!RequestValidator.IdValidate(id))
            {
                return BadRequest();
            }
            _userService.DeleteUser(id);
            HttpContext.Response.Cookies.Delete("snezhok_cookie");
            HttpContext.Response.Cookies.Delete("user_cookie");
            return Ok();
        }

        [HttpPost("AddKarma/{fromUserId}/{toUserid}")]
        public ActionResult<bool> AddKarma(int fromUserId, int toUserid)
        {
            if (!RequestValidator.IdValidate(fromUserId) || !RequestValidator.IdValidate(toUserid))
            {
                return BadRequest();
            }
            bool result = _userService.AddKarma(fromUserId, toUserid);
            return Ok(result);
        }

        [HttpDelete("RemoveVoteKarma/{fromUserId}/{toUserid}")]
        public ActionResult<bool> RemoveVoteKarma(int fromUserId, int toUserid)
        {
            if (!RequestValidator.IdValidate(fromUserId) || !RequestValidator.IdValidate(toUserid))
            {
                return BadRequest();
            }
            bool result = _userService.RemoveVoteKarma(fromUserId, toUserid);
            return Ok(result);
        }
    }
}
