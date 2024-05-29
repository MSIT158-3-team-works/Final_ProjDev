using System;
using System.Collections.Generic;

namespace projRESTfulApiFitConnect.Models;

public partial class TclassSort訓練
{
    public int ClassSort2Id { get; set; }

    public string ClassSort2Detail { get; set; } = null!;

    public virtual ICollection<Tclass> Tclasses { get; set; } = new List<Tclass>();
}
