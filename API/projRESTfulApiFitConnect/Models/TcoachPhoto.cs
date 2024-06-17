using System;
using System.Collections.Generic;

namespace projRESTfulApiFitConnect.Models;

public partial class TcoachPhoto
{
    public int CoachPhotoId { get; set; }

    public int Id { get; set; }

    public string? CoachPhoto { get; set; }

    public virtual TIdentity IdNavigation { get; set; } = null!;
}
