using System;
using System.Collections.Generic;

namespace MangXaHoi.Models;

public partial class Safe
{
    public int Id { get; set; }

    public int Contentid { get; set; }

    public int Userid { get; set; }

    public DateTime? Date { get; set; }

    public virtual Content Content { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
