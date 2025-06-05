using MyBlockForumServer.DataBase.Entities;
using MyBlockForumServer.DataBase.Repositories;

namespace MyBlockForumServer.DataBase.Services
{
    public class MessageService : IMessageService
    {
        private IRepository<Message> _messageRepository;
        public MessageService(IRepository<Message> messageRepository)
        {
            _messageRepository = messageRepository;
        }

        public void SendMessage(Message message)
        {
            _messageRepository.Create(message);
            _messageRepository.Save();
        }

        public void EditMessage(Message message)
        {
            _messageRepository.Update(message);
            _messageRepository.Save();
        }

        public void DeleteMessage(int id)
        {
            _messageRepository.Delete(id);
            _messageRepository.Save();
        }

        public Message? GetMessage(int id)
        {
            return _messageRepository.Get(id);
        }

        public IEnumerable<Message> GetAllMessagesByUser(int userId)
        {
            return _messageRepository.GetList().Where(l => l.UserId == userId);
        }

        public IEnumerable<Message> GetAllMessagesFromThread(int threadId)
        {
            return _messageRepository.GetList().Where(l => l.ThreadId == threadId);
        }
    }
}
