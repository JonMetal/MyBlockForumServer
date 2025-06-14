using Microsoft.EntityFrameworkCore;
using MyBlockForumServer.DataBase.Entities;

namespace MyBlockForumServer.DataBase.Repositories
{
    public class UserThreadRepository : AbstractRepository<UserThread>
    {
        public override void Create(UserThread entity)
        {
            _db.UserThreads.Add(entity);
        }

        public override void Delete(int id)
        {
            UserThread? userThread = _db.UserThreads.FirstOrDefault(l => l.Id == id);
            if (userThread != null)
            {
                _db.UserThreads.Remove(userThread);
            }
        }

        public override UserThread? Get(int id)
        {
            return _db.UserThreads.FirstOrDefault(l => l.Id == id);
        }

        public override IEnumerable<UserThread> GetList()
        {
            return _db.UserThreads.Include(l => l.Thread).Include(l => l.User);
        }

        public override void Update(UserThread entity)
        {
            _db.UserThreads.Entry(entity).State = EntityState.Modified;
        }
    }
}
