using System;
using System.Collections.Generic;

namespace MangXaHoi.Models;

public partial class Friendship
{
    public int Id { get; set; }

    public int Userid1 { get; set; }

    public bool? Status { get; set; }

    public int Userid2 { get; set; }

    public DateTime? Date { get; set; }

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

    public virtual User Userid1Navigation { get; set; } = null!;

    public virtual User Userid2Navigation { get; set; } = null!;
}
