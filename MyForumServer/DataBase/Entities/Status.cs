using System.Text.Json.Serialization;

namespace MyBlockForumServer.DataBase.Entities;

public partial class Status
{
    public int Id { get; set; }

    public string? Title { get; set; }

    [JsonIgnore]
    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
