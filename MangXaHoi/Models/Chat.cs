using System;
using System.Collections.Generic;

namespace MangXaHoi.Models;

public partial class Chat
{
    public int Id { get; set; }

    public string? Content { get; set; }

    public int Userid { get; set; }

    public DateTime? Date { get; set; }

    public int Messagesid { get; set; }

    public virtual Message Messages { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
