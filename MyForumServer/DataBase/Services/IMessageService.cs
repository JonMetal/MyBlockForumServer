using MyBlockForumServer.DataBase.Entities;

namespace MyBlockForumServer.DataBase.Services
{
    public interface IMessageService
    {
        void DeleteMessage(int id);
        void EditMessage(Message message);
        IEnumerable<Message> GetAllMessagesByUser(int userId);
        IEnumerable<Message> GetAllMessagesFromThread(int threadId);
        Message? GetMessage(int id);
        void SendMessage(Message message);
    }
}