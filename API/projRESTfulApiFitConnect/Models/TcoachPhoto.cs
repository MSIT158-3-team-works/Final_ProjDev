using System;
using System.Collections.Generic;

namespace projRESTfulApiFitConnect.Models;

public partial class TcoachPhoto
{
    public int CoachPhotoId { get; set; }

    public int ExpertId { get; set; }

    public string? CoachPhoto { get; set; }

    public virtual TcoachExpert Expert { get; set; } = null!;
}
