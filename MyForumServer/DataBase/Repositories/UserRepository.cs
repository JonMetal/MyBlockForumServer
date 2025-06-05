using Microsoft.EntityFrameworkCore;
using MyBlockForumServer.DataBase.Entities;

namespace MyBlockForumServer.DataBase.Repositories
{
    public class UserRepository : AbstractRepository<User>
    {
        public override void Create(User entity)
        {
            _db.Users.Add(entity);
        }

        public override void Delete(int id)
        {
            User? user = _db.Users.FirstOrDefault(l => l.Id == id);
            if (user != null)
            {
                _db.Users.Remove(user);
            }
        }

        public override User? Get(int id)
        {
            return _db.Users.Include(l => l.Status).FirstOrDefault(l => l.Id == id);
        }

        public override IEnumerable<User> GetList()
        {
            return _db.Users.Include(l => l.Status);
        }

        public override void Update(User entity)
        {
            _db.Users.Entry(entity).State = EntityState.Modified;
        }
    }
}
