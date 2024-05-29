using System;
using System.Collections.Generic;

namespace projRESTfulApiFitConnect.Models;

public partial class TclassSort有氧
{
    public int ClassSort1Id { get; set; }

    public string ClassSort1Detail { get; set; } = null!;

    public virtual ICollection<Tclass> Tclasses { get; set; } = new List<Tclass>();
}
