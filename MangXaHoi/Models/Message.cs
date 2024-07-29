using System;
using System.Collections.Generic;

namespace MangXaHoi.Models;

public partial class Message
{
    public int Id { get; set; }

    public int Useridgui { get; set; }

    public int Useridnhan { get; set; }

    public bool? Status { get; set; }

    public string? ContentPhoto { get; set; }

    public DateTime? Date { get; set; }

    public virtual ICollection<Chat> Chats { get; set; } = new List<Chat>();

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

    public virtual User UseridguiNavigation { get; set; } = null!;

    public virtual User UseridnhanNavigation { get; set; } = null!;
}
