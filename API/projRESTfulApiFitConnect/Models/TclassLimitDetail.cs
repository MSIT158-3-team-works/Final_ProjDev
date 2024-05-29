using System;
using System.Collections.Generic;

namespace projRESTfulApiFitConnect.Models;

public partial class TclassLimitDetail
{
    public int ClassLimitedId { get; set; }

    public string Describe { get; set; } = null!;

    public virtual ICollection<Tclass> Tclasses { get; set; } = new List<Tclass>();
}
