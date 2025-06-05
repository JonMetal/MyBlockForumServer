using System.Text.Json.Serialization;

namespace MyBlockForumServer.DataBase.Entities;

public partial class ThreadTheme
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    [JsonIgnore]
    public virtual ICollection<Thread> Threads { get; set; } = new List<Thread>();
}
