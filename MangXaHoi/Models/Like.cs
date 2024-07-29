using System;
using System.Collections.Generic;

namespace MangXaHoi.Models;

public partial class Like
{
    public int Id { get; set; }

    public int Userid { get; set; }

    public int Contentid { get; set; }

    public int? Commentid { get; set; }

    public DateTime? Date { get; set; }

    public virtual Comment? Comment { get; set; }

    public virtual Content Content { get; set; } = null!;

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

    public virtual User User { get; set; } = null!;
}
