using System;
using System.Collections.Generic;

namespace projRESTfulApiFitConnect.Models;

public partial class TcoachExpert
{
    public int ExpertId { get; set; }

    public int CoachId { get; set; }

    public int ClassId { get; set; }

    public virtual Tclass Class { get; set; } = null!;

    public virtual TIdentity Coach { get; set; } = null!;
}
