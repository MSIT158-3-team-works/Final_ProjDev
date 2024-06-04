using System;
using System.Collections.Generic;

namespace projRESTfulApiFitConnect.Models;

public partial class TclassSchedule
{
    public int ClassScheduleId { get; set; }

    public int ClassId { get; set; }

    public int CoachId { get; set; }

    public int FieldId { get; set; }

    public DateTime CourseDate { get; set; }

    public int CourseTimeId { get; set; }

    public int MaxStudent { get; set; }

    public int ClassStatusId { get; set; }

    public decimal ClassPayment { get; set; }

    public bool CoachPayment { get; set; }

    public virtual Tclass Class { get; set; } = null!;

    public virtual TclassStatusDetail ClassStatus { get; set; } = null!;

    public virtual TIdentity Coach { get; set; } = null!;

    public virtual TtimesDetail CourseTime { get; set; } = null!;

    public virtual Tfield Field { get; set; } = null!;

    public virtual ICollection<TclassReserve> TclassReserves { get; set; } = new List<TclassReserve>();
}
