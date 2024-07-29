using System;
using System.Collections.Generic;

namespace MangXaHoi.Models;

public partial class Notification
{
    public int Id { get; set; }

    public int? Commentid { get; set; }

    public int? Likeid { get; set; }

    public int? Friendshipid { get; set; }

    public bool? Status { get; set; }

    public int? Contentid { get; set; }

    public int? Messageid { get; set; }

    public int? Storyid { get; set; }

    public int Userid { get; set; }

    public virtual Comment? Comment { get; set; }

    public virtual Content? Content { get; set; }

    public virtual Friendship? Friendship { get; set; }

    public virtual Like? Like { get; set; }

    public virtual Message? Message { get; set; }

    public virtual Story? Story { get; set; }

    public virtual User User { get; set; } = null!;
}
