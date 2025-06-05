namespace MyBlockForumServer.DataBase.Repositories
{
    public abstract class AbstractRepository<T> : IRepository<T> where T : class
    {
        protected MyBlockForumDbContext _db;
        private bool _disposed = false;

        protected AbstractRepository()
        {
            _db = new();
        }

        public abstract void Create(T entity);

        public abstract void Delete(int id);

        public virtual void Dispose()
        {
            if (!_disposed)
            {
                _db.Dispose();
            }
            _disposed = true;
        }

        public abstract T? Get(int id);

        public abstract IEnumerable<T> GetList();

        public abstract void Update(T entity);

        public virtual void Save()
        {
            _db.SaveChanges();
        }
    }
}
