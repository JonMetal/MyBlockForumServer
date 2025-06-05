using MyBlockForumServer.DataBase.Entities;

namespace MyBlockForumServer.DataBase.Services
{
    public interface IUserService
    {
        void AddThreadUser(int userId, int threadId);
        string Login(string login, string password);
        void CreateUser(User user);
        void DeleteUser(int id);
        IEnumerable<Role> GetAllRoles();
        IEnumerable<Status> GetAllStatuses();
        User? GetUser(int id);
        Role? GetUserRole(int id);
        Status? GetUserStatus(int id);
        IEnumerable<Entities.Thread> GetUserThreads(int id);
        void SetUser(User user);
    }
}