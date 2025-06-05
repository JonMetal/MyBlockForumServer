namespace MyBlockForumServer.DataBase.Repositories
{
    public interface IRepository<T> : IDisposable where T : class
    {
        public void Create(T entity);

        public void Delete(int id);

        public void Update(T entity);

        public T? Get(int id);

        public IEnumerable<T> GetList();

        void Save();
    }
}
