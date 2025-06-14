using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyBlockForumServer.DataBase.Entities;
using MyBlockForumServer.DataBase.Services;
using MyBlockForumServer.Tools;
using Thread = MyBlockForumServer.DataBase.Entities.Thread;

namespace MyBlockForumServer.Controllers
{

    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class ThreadsController : Controller
    {
        IThreadService _threadService;

        public ThreadsController(IThreadService threadService)
        {
            _threadService = threadService;
        }

        [HttpGet("AllThreads", Name = "GetThreads")]
        public IEnumerable<Thread> GetThreads()
        {
            return _threadService.GetAllThreads();
        }

        [HttpGet("AllThreadThemes", Name = "GetThreadThemes")]
        public IEnumerable<ThreadTheme> GetThreadThemes()
        {
            return _threadService.GetAllThreadThemes();
        }

        [HttpGet("{id}", Name = "GetThreadById")]
        public ActionResult<Thread?> GetThread(int id)
        {
            if (!RequestValidator.IdValidate(id))
            {
                return BadRequest();
            }
            return Ok(_threadService.GetThread(id));
        }

        [HttpGet("ThreadThemes/{id}")]
        public ActionResult<ThreadTheme?> GetThreadTheme(int id)
        {
            if (!RequestValidator.IdValidate(id))
            {
                return BadRequest();
            }
            return Ok(_threadService.GetThreadTheme(id));
        }

        [HttpGet("ThreadsByTheme/{id}")]
        public ActionResult<IEnumerable<Thread>> GetThreadByTheme(int id)
        {
            if (!RequestValidator.IdValidate(id))
            {
                return BadRequest();
            }
            return Ok(_threadService.GetThreadByTheme(id));
        }

        [HttpPost]
        public ActionResult<Thread> Post(Thread thread)
        {
            if (!RequestValidator.ObjectValidate(thread) || !RequestValidator.IdValidate(thread.ThreadThemeId))
            {
                return BadRequest();
            }
            _threadService.CreateThread(thread);
            return Ok(thread);
        }

        [HttpPut]
        public ActionResult<Thread> Put(Thread thread)
        {
            if (!RequestValidator.ObjectValidate(thread))
            {
                return BadRequest();
            }
            _threadService.UpdateThread(thread);
            return Ok(thread);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            if (!RequestValidator.IdValidate(id))
            {
                return BadRequest();
            }
            _threadService.DeleteThread(id);
            return Ok();
        }
    }
}
