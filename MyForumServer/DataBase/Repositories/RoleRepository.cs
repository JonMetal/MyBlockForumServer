using Microsoft.EntityFrameworkCore;
using MyBlockForumServer.DataBase.Entities;

namespace MyBlockForumServer.DataBase.Repositories
{
    public class RoleRepository : AbstractRepository<Role>
    {
        public RoleRepository() : base() { }
        public override void Create(Role entity)
        {
            _db.Roles.Add(entity);
        }

        public override void Delete(int id)
        {
            Role? role = _db.Roles.FirstOrDefault(l => l.Id == id);
            if (role != null)
            {
                _db.Roles.Remove(role);
            }
        }

        public override Role? Get(int id)
        {
            return _db.Roles.FirstOrDefault(l => l.Id == id);
        }

        public override IEnumerable<Role> GetList()
        {
            return _db.Roles;
        }

        public override void Update(Role entity)
        {
            _db.Roles.Entry(entity).State = EntityState.Modified;
        }
    }
}
