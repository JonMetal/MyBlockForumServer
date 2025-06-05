using System.Text.Json.Serialization;

namespace MyBlockForumServer.DataBase.Entities;

public partial class Thread
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public int ThreadThemeId { get; set; }

    [JsonIgnore]
    public virtual ICollection<Message> Messages { get; set; } = new List<Message>();

    [JsonIgnore]
    public virtual ThreadTheme? ThreadTheme { get; set; }

    [JsonIgnore]
    public virtual ICollection<UserThread> UserThreads { get; set; } = new List<UserThread>();
}
