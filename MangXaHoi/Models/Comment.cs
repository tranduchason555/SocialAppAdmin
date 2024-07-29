using System;
using System.Collections.Generic;

namespace MangXaHoi.Models;

public partial class Comment
{
    public int Id { get; set; }

    public string? Comment1 { get; set; }

    public int Userid { get; set; }

    public DateTime? Date { get; set; }

    public int Contentid { get; set; }

    public virtual Content Content { get; set; } = null!;

    public virtual ICollection<Like> Likes { get; set; } = new List<Like>();

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

    public virtual User User { get; set; } = null!;
}
