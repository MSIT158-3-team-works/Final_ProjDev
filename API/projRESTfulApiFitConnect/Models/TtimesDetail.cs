using System;
using System.Collections.Generic;

namespace projRESTfulApiFitConnect.Models;

public partial class TtimesDetail
{
    public int TimeId { get; set; }

    public string TimeName { get; set; } = null!;

    public virtual ICollection<TclassSchedule> TclassSchedules { get; set; } = new List<TclassSchedule>();
}
