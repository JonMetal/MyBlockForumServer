using Microsoft.EntityFrameworkCore;
using MyBlockForumServer.DataBase.Entities;
using Thread = MyBlockForumServer.DataBase.Entities.Thread;

namespace MyBlockForumServer.DataBase.Repositories
{
    public class ThreadRepository : AbstractRepository<Thread>
    {
        public override void Create(Thread entity)
        {
            if (entity.ThreadThemeId > 0)
            {
                ThreadTheme? threadTheme = _db.ThreadThemes.FirstOrDefault(t => t.Id == entity.ThreadThemeId);
                if (threadTheme == null)
                {
                    throw new InvalidOperationException($"ThreadTheme with Id {entity.ThreadThemeId} not found.");
                }
                entity.ThreadTheme = threadTheme;
                _db.Entry(entity.ThreadTheme).State = EntityState.Unchanged;
            }
            else
            {
                throw new InvalidOperationException("ThreadThemeId must be specified.");
            }
            _db.Threads.Add(entity);
        }

        public override void Delete(int id)
        {
            Thread? thread = _db.Threads.FirstOrDefault(l => l.Id == id);
            if (thread != null)
            {
                _db.Threads.Remove(thread);
            }
        }

        public override Thread? Get(int id)
        {
            return _db.Threads.Include(l => l.ThreadTheme).FirstOrDefault(l => l.Id == id);
        }

        public override IEnumerable<Thread> GetList()
        {
            return _db.Threads.Include(l => l.ThreadTheme);
        }

        public override void Update(Thread entity)
        {
            _db.Threads.Entry(entity).State = EntityState.Modified;
        }
    }
}
