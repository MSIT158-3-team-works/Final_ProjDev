using System;
using System.Collections.Generic;

namespace projRESTfulApiFitConnect.Models;

public partial class TnewsComment
{
    public int NewsCommentId { get; set; }

    public int NewsId { get; set; }

    public int CommenterId { get; set; }

    public string NewsComment { get; set; } = null!;

    public virtual TIdentity Commenter { get; set; } = null!;

    public virtual Tnews News { get; set; } = null!;
}
