using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyBlockForumServer.DataBase.Entities;
using MyBlockForumServer.DataBase.Services;
using MyBlockForumServer.Tools;

namespace MyBlockForumServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class MessagesController : Controller
    {
        private IMessageService _messageService;

        public MessagesController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        [HttpGet("MessagesByUser/{id}")]
        public ActionResult<IEnumerable<Message>> GetMessagesByUser(int id)
        {
            if (!RequestValidator.IdValidate(id))
            {
                return BadRequest();
            }
            return Ok(_messageService.GetAllMessagesByUser(id));
        }

        [HttpGet("ThreadMessages/{id}")]
        public ActionResult<IEnumerable<Message>> GetMessagesFromThread(int id)
        {
            if (!RequestValidator.IdValidate(id))
            {
                return BadRequest();
            }
            return Ok(_messageService.GetAllMessagesFromThread(id));
        }

        [HttpPost]
        public ActionResult<Message> SendMessage(Message message)
        {
            if (!RequestValidator.ObjectValidate(message))
            {
                return BadRequest();
            }
            _messageService.SendMessage(message);
            return Ok(message);
        }

        [HttpPut]
        public ActionResult<Message> EditMessage(Message message)
        {
            if (!RequestValidator.ObjectValidate(message))
            {
                return BadRequest();
            }
            _messageService.EditMessage(message);
            return Ok(message);
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            if (!RequestValidator.IdValidate(id))
            {
                return BadRequest();
            }
            _messageService.DeleteMessage(id);
            return Ok();
        }
    }
}
