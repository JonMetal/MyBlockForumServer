using MyBlockForumServer.DataBase.Entities;
using MyBlockForumServer.DataBase.Repositories;
using Thread = MyBlockForumServer.DataBase.Entities.Thread;

namespace MyBlockForumServer.DataBase.Services
{
    public class ThreadService : IThreadService
    {
        IRepository<Thread> _threadRepository;
        IRepository<ThreadTheme> _threadThemeRepository;

        public ThreadService(IRepository<Thread> threadRepository, IRepository<ThreadTheme> threadThemeRepository)
        {
            _threadRepository = threadRepository;
            _threadThemeRepository = threadThemeRepository;
        }

        public void CreateThread(Thread thread)
        {
            _threadRepository.Create(thread);
            _threadRepository.Save();
        }

        public void DeleteThread(int threadId)
        {
            _threadRepository.Delete(threadId);
            _threadRepository.Save();
        }

        public void UpdateThread(Thread thread)
        {
            _threadRepository.Update(thread);
            _threadRepository.Save();
        }

        public Thread? GetThread(int id)
        {
            return _threadRepository.Get(id);
        }

        public ThreadTheme? GetThreadTheme(int id)
        {
            return _threadThemeRepository.Get(id);
        }
        public IEnumerable<Thread> GetAllThreads()
        {
            return _threadRepository.GetList();
        }

        public IEnumerable<Thread> GetThreadByTheme(int id)
        {
            return _threadRepository.GetList().Where(l => l.ThreadThemeId == id);
        }

        public IEnumerable<ThreadTheme> GetAllThreadThemes()
        {
            return _threadThemeRepository.GetList();
        }
    }
}
