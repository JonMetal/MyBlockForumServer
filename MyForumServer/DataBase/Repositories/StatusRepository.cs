using Microsoft.EntityFrameworkCore;
using MyBlockForumServer.DataBase.Entities;

namespace MyBlockForumServer.DataBase.Repositories
{
    public class StatusRepository : AbstractRepository<Status>
    {
        public override void Create(Status entity)
        {
            _db.Statuses.Add(entity);
        }

        public override void Delete(int id)
        {
            Status? status = _db.Statuses.FirstOrDefault(l => l.Id == id);
            if (status != null)
            {
                _db.Statuses.Remove(status);
            }
        }

        public override Status? Get(int id)
        {
            return _db.Statuses.FirstOrDefault(l => l.Id == id);
        }

        public override IEnumerable<Status> GetList()
        {
            return _db.Statuses;
        }

        public override void Update(Status entity)
        {
            _db.Statuses.Entry(entity).State = EntityState.Modified;
        }
    }
}
