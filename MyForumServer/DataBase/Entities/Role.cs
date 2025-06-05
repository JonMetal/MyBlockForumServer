using System.Text.Json.Serialization;

namespace MyBlockForumServer.DataBase.Entities;

public partial class Role
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    [JsonIgnore]
    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
