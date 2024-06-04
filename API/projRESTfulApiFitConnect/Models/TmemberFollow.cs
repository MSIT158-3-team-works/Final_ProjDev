using System;
using System.Collections.Generic;

namespace projRESTfulApiFitConnect.Models;

public partial class TmemberFollow
{
    public int FollowId { get; set; }

    public int MemberId { get; set; }

    public int CoachId { get; set; }

    public int StatusId { get; set; }

    public virtual TIdentity Coach { get; set; } = null!;

    public virtual TIdentity Member { get; set; } = null!;

    public virtual TmemberStatusDetail Status { get; set; } = null!;
}
