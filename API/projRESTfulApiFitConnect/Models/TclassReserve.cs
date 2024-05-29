using System;
using System.Collections.Generic;

namespace projRESTfulApiFitConnect.Models;

public partial class TclassReserve
{
    public int ReserveId { get; set; }

    public int ClassScheduleId { get; set; }

    public int MemberId { get; set; }

    public bool PaymentStatus { get; set; }

    public bool? ReserveStatus { get; set; }

    public virtual TclassSchedule ClassSchedule { get; set; } = null!;

    public virtual TIdentity Member { get; set; } = null!;

    public virtual ICollection<TmemberRateClass> TmemberRateClasses { get; set; } = new List<TmemberRateClass>();
}
