using MyBlockForumServer.DataBase.Entities;

namespace MyBlockForumServer.DataBase.Services
{
    public interface IThreadService
    {
        void CreateThread(Entities.Thread thread);
        void DeleteThread(int threadId);
        IEnumerable<Entities.Thread> GetAllThreads();
        IEnumerable<ThreadTheme> GetAllThreadThemes();
        Entities.Thread? GetThread(int id);
        IEnumerable<Entities.Thread> GetThreadByTheme(int id);
        ThreadTheme? GetThreadTheme(int id);
        void UpdateThread(Entities.Thread thread);
    }
}