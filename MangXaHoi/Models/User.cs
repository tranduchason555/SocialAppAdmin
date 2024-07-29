using System;
using System.Collections.Generic;

namespace MangXaHoi.Models;

public partial class User
{
    public int Id { get; set; }

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string? Fullname { get; set; }

    public string? Address { get; set; }

    public string? Age { get; set; }

    public string? Phone { get; set; }

    public string? Photo { get; set; }

    public int? Roleid { get; set; }

    public virtual ICollection<Chat> Chats { get; set; } = new List<Chat>();

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual ICollection<Content> Contents { get; set; } = new List<Content>();

    public virtual ICollection<Friendship> FriendshipUserid1Navigations { get; set; } = new List<Friendship>();

    public virtual ICollection<Friendship> FriendshipUserid2Navigations { get; set; } = new List<Friendship>();

    public virtual ICollection<Like> Likes { get; set; } = new List<Like>();

    public virtual ICollection<Message> MessageUseridguiNavigations { get; set; } = new List<Message>();

    public virtual ICollection<Message> MessageUseridnhanNavigations { get; set; } = new List<Message>();

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

    public virtual ICollection<Report> Reports { get; set; } = new List<Report>();

    public virtual Role? Role { get; set; }

    public virtual ICollection<Safe> Saves { get; set; } = new List<Safe>();

    public virtual ICollection<Story> Stories { get; set; } = new List<Story>();
}
