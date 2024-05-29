using System;
using System.Collections.Generic;

namespace projRESTfulApiFitConnect.Models;

public partial class TclassStatusDetail
{
    public int ClassStatusId { get; set; }

    public string ClassStatusDiscribe { get; set; } = null!;

    public virtual ICollection<TclassSchedule> TclassSchedules { get; set; } = new List<TclassSchedule>();
}
