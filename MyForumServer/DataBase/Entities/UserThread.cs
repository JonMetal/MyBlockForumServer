using System.Text.Json.Serialization;

namespace MyBlockForumServer.DataBase.Entities;

public partial class UserThread
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int ThreadId { get; set; }

    [JsonIgnore]
    public virtual Thread? Thread { get; set; }

    [JsonIgnore]
    public virtual User? User { get; set; }
}
