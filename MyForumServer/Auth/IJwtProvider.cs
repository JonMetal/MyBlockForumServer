using MyBlockForumServer.DataBase.Entities;

namespace MyBlockForumServer.Auth
{
    public interface IJwtProvider
    {
        string GenerateToken(User user);
    }
}