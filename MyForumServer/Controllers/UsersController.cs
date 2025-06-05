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

        [HttpGet("Users/{id}")]
        public ActionResult<User> GetUser(int id)
        {
            if (!RequestValidator.IdValidate(id))
            {
                return BadRequest();
            }
            return Ok(_userService.GetUser(id));
        }

        [HttpPost("Users/Login")]
        [AllowAnonymous]
        public ActionResult<string> Login([FromBody] LoginRequestDetails loginRequestDetails)
        {
            var token = _userService.Login(loginRequestDetails.Login, loginRequestDetails.Password);

            CookieOptions cookieOptions = new CookieOptions
            {
                HttpOnly = false,
                Secure = false,
            };
            HttpContext.Response.Cookies.Append("snezhok_cookie", token, cookieOptions);

            return Ok(token);
        }

        [HttpGet("AllStatuses")]
        public IEnumerable<Status> GetAllStatuses()
        {
            return _userService.GetAllStatuses();
        }

        [HttpGet("AllRoles")]
        public IEnumerable<Role> GetAllRoles()
        {
            return _userService.GetAllRoles();
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
                return BadRequest();
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
                return BadRequest();
            }
            _userService.AddThreadUser(userId, threadId);
            return Ok();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult<User> CreateUser(User user)
        {
            if (!RequestValidator.ObjectValidate(user))
            {
                return BadRequest();
            }
            _userService.CreateUser(user);
            return Ok(user);
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

        [HttpDelete]
        public ActionResult DeleteUser(int id)
        {
            if (!RequestValidator.IdValidate(id))
            {
                return BadRequest();
            }
            _userService.DeleteUser(id);
            return Ok();
        }
    }
}
