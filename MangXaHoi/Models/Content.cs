using System;
using System.Collections.Generic;

namespace MangXaHoi.Models;

public partial class Content
{
    public int Id { get; set; }

    public string? Content1 { get; set; }

    public DateTime Date { get; set; }

    public bool? Status { get; set; }

    public int Userid { get; set; }

    public string? ContentPhoto { get; set; }

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual ICollection<Like> Likes { get; set; } = new List<Like>();

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

    public virtual ICollection<Report> Reports { get; set; } = new List<Report>();

    public virtual ICollection<Safe> Saves { get; set; } = new List<Safe>();

    public virtual User User { get; set; } = null!;
}
