using System;
using System.Collections.Generic;

namespace MangXaHoi.Models;

public partial class Story
{
    public int Id { get; set; }

    public string? Content { get; set; }

    public int Userid { get; set; }

    public bool? Status { get; set; }

    public string? Photo { get; set; }

    public DateTime? Date { get; set; }

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

    public virtual User User { get; set; } = null!;
}
