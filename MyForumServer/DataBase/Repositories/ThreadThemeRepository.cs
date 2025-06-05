using Microsoft.EntityFrameworkCore;
using MyBlockForumServer.DataBase.Entities;

namespace MyBlockForumServer.DataBase.Repositories
{
    public class ThreadThemeRepository : AbstractRepository<ThreadTheme>
    {
        public override void Create(ThreadTheme entity)
        {
            _db.ThreadThemes.Add(entity);
        }

        public override void Delete(int id)
        {
            ThreadTheme? threadTheme = _db.ThreadThemes.FirstOrDefault(l => l.Id == id);
            if (threadTheme != null)
            {
                _db.ThreadThemes.Remove(threadTheme);
            }
        }

        public override ThreadTheme? Get(int id)
        {
            return _db.ThreadThemes.FirstOrDefault(l => l.Id == id);
        }

        public override IEnumerable<ThreadTheme> GetList()
        {
            return _db.ThreadThemes;
        }

        public override void Update(ThreadTheme entity)
        {
            _db.ThreadThemes.Entry(entity).State = EntityState.Modified;
        }
    }
}
