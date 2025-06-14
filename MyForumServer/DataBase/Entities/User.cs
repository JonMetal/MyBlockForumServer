﻿using System.Text.Json.Serialization;

namespace MyBlockForumServer.DataBase.Entities;

public partial class User
{
    public int Id { get; set; }

    public string? Login { get; set; }

    public string? Password { get; set; }

    public string? Nickname { get; set; }

    public string? Description { get; set; }

    public int? Karma { get; set; } = 0;

    public int StatusId { get; set; } = 1;

    public int RoleId { get; set; } = 1;

    [JsonIgnore]
    public virtual ICollection<Message> Messages { get; set; } = new List<Message>();

    [JsonIgnore]
    public virtual Role? Role { get; set; }

    [JsonIgnore]
    public virtual Status? Status { get; set; }

    [JsonIgnore]
    public virtual ICollection<UserThread> UserThreads { get; set; } = new List<UserThread>();

    [JsonIgnore]
    public virtual ICollection<User> FromUsers { get; set; } = new List<User>();

    [JsonIgnore]
    public virtual ICollection<User> ToUsers { get; set; } = new List<User>();
}
