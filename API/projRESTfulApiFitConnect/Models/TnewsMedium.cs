using System;
using System.Collections.Generic;

namespace projRESTfulApiFitConnect.Models;

public partial class TnewsMedium
{
    public int NewsPhotoId { get; set; }

    public int NewsId { get; set; }

    public string NewsPhoto { get; set; } = null!;

    public string? NewsVideolink { get; set; }

    public virtual Tnews News { get; set; } = null!;
}
