using System;
using System.Collections.Generic;

namespace projRESTfulApiFitConnect.Models;

public partial class TcoachInfoId
{
    public int CoachInfoId { get; set; }

    public string? CoachIntro { get; set; }

    public int CoachId { get; set; }

    public virtual TIdentity Coach { get; set; } = null!;
}
