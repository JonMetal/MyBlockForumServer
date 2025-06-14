using MyBlockForumServer.DataBase.Entities;

namespace MyBlockForumServer.DataBase.Services
{
    public interface IUserService
    {
        bool AddKarma(int fromUserId, int toUserId);
        void AddThreadUser(int userId, int threadId);
        User CreateUser(User user);
        void DeleteUser(int id);
        IEnumerable<Role> GetAllRoles();
        IEnumerable<Status> GetAllStatuses();
        User? GetUser(int id);
        User? GetUserByLogin(string login);
        Role? GetUserRole(int id);
        IEnumerable<User> GetUsers();
        IEnumerable<User?> GetUsersFromThread(int threadId);
        Status? GetUserStatus(int id);
        IEnumerable<Entities.Thread?> GetUserThreads(int id);
        string Login(string login, string password);
        bool RemoveVoteKarma(int fromUserId, int toUserId);
        void SetUser(User user);
    }
}