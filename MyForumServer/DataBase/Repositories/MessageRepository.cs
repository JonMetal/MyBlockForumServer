using Microsoft.EntityFrameworkCore;
using MyBlockForumServer.DataBase.Entities;

namespace MyBlockForumServer.DataBase.Repositories
{
    public class MessageRepository : AbstractRepository<Message>
    {
        public MessageRepository() : base() { }
        public override void Create(Message entity)
        {
            _db.Messages.Add(entity);
        }

        public override void Delete(int id)
        {
            Message? message = _db.Messages.FirstOrDefault(l => l.Id == id);
            if (message != null)
            {
                _db.Messages.Remove(message);
            }
        }

        public override Message? Get(int id)
        {
            return _db.Messages.FirstOrDefault(l => l.Id == id);
        }

        public override IEnumerable<Message> GetList()
        {
            return _db.Messages;
        }

        public override void Update(Message entity)
        {
            _db.Messages.Entry(entity).State = EntityState.Modified;
        }
    }
}
