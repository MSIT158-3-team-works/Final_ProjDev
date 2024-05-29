using System;
using System.Collections.Generic;

namespace projRESTfulApiFitConnect.Models;

public partial class TtimesDetail
{
    public int TimeId { get; set; }

    public TimeSpan TimeName { get; set; }

    public virtual ICollection<TclassSchedule> TclassSchedules { get; set; } = new List<TclassSchedule>();
}
