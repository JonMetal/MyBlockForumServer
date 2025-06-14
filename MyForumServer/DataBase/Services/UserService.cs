using MyBlockForumServer.Auth;
using MyBlockForumServer.DataBase.Entities;
using MyBlockForumServer.DataBase.Repositories;
using MyBlockForumServer.Tools;
using Thread = MyBlockForumServer.DataBase.Entities.Thread;

namespace MyBlockForumServer.DataBase.Services
{
    public class UserService : IUserService
    {
        IRepository<Role> _roleRepository;
        IRepository<Status> _statusRepository;
        IRepository<UserThread> _userThreadRepository;
        IRepository<User> _userRepository;
        IJwtProvider _jwtProvider;
        public UserService(IRepository<Role> roleRepository, IRepository<Status> statusRepository,
            IRepository<UserThread> userThreadRepository, IRepository<User> userRepository, IJwtProvider jwtProvider)
        {
            _roleRepository = roleRepository;
            _statusRepository = statusRepository;
            _userThreadRepository = userThreadRepository;
            _userRepository = userRepository;
            _jwtProvider = jwtProvider;
        }

        public User? GetUser(int id)
        {
            return _userRepository.Get(id);
        }

        public User? GetUserByLogin(string login)
        {
            return _userRepository.GetList().FirstOrDefault(l => l.Login == login);
        }

        public IEnumerable<User> GetUsers()
        {
            return _userRepository.GetList();
        }

        public string Login(string login, string password)
        {
            User? user = _userRepository.GetList().FirstOrDefault(l => l.Login == login);

            if (user == null)
            {
                throw new Exception("There is no such login");
            }

            if (Hash.GetHash(login, password) != user.Password)
            {
                throw new Exception("Failed to Login");
            }

            string token = _jwtProvider.GenerateToken(user);

            return token;
        }

        public User CreateUser(User user)
        {
            user.Password = Hash.GetHash(user.Login ?? "", user.Password ?? "");
            _userRepository.Create(user);
            _userRepository.Save();
            return user;
        }

        public void SetUser(User user)
        {
            _userRepository.Update(user);
            _userRepository.Save();
        }

        public void DeleteUser(int id)
        {
            _userRepository.Delete(id);
            _userRepository.Save();
        }

        public Role? GetUserRole(int id)
        {
            return _roleRepository.Get(id);
        }

        public IEnumerable<Role> GetAllRoles()
        {
            return _roleRepository.GetList();
        }

        public Status? GetUserStatus(int id)
        {
            User? user = _userRepository.Get(id);
            return _statusRepository.Get(user?.StatusId ?? 0);
        }

        public IEnumerable<Status> GetAllStatuses()
        {
            return _statusRepository.GetList();
        }

        public void AddThreadUser(int userId, int threadId)
        {
            _userThreadRepository.Create(new UserThread() { UserId = userId, ThreadId = threadId });
            _userThreadRepository.Save();
        }

        public IEnumerable<Thread?> GetUserThreads(int id)
        {
            return _userThreadRepository.GetList().Where(l => l.UserId == id).Select(l => l.Thread);
        }

        public bool AddKarma(int fromUserId, int toUserId)
        {
            User? userFrom = _userRepository.Get(fromUserId);
            User? userTo = _userRepository.Get(toUserId);
            if (userFrom != null && userTo != null)
            {
                if (userTo.FromUsers.Contains(userFrom)) { return false; }
                userTo.Karma += 1;
                userTo.FromUsers.Add(userFrom);
                _userRepository.Save();
                return true;
            }
            else
            {
                throw new Exception("One of users is not defined");
            }
        }

        public bool RemoveVoteKarma(int fromUserId, int toUserId)
        {
            User? userFrom = _userRepository.Get(fromUserId);
            User? userTo = _userRepository.Get(toUserId);
            if (userFrom != null && userTo != null)
            {
                if (!userTo.FromUsers.Contains(userFrom)) { return false; }
                userTo.Karma -= 1;
                userTo.FromUsers.Remove(userFrom);
                _userRepository.Save();
                return true;
            }
            else
            {
                throw new Exception("One of users is not defined");
            }
        }

        public IEnumerable<User?> GetUsersFromThread(int threadId)
        {
            return _userThreadRepository.GetList().Where(l => l.ThreadId == threadId).Select(l => l.User);
        }
    }
}
