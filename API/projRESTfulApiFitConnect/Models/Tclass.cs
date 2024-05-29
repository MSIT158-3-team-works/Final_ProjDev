using System;
using System.Collections.Generic;

namespace projRESTfulApiFitConnect.Models;

public partial class Tclass
{
    public int ClassId { get; set; }

    public int? ClassSort1Id { get; set; }

    public int? ClassSort2Id { get; set; }

    public string ClassName { get; set; } = null!;

    public string? ClassIntroduction { get; set; }

    public int LimitedGender { get; set; }

    public string? ClassPhoto { get; set; }

    public bool ClassStatus { get; set; }

    public virtual TclassSort有氧? ClassSort1 { get; set; }

    public virtual TclassSort訓練? ClassSort2 { get; set; }

    public virtual TclassLimitDetail LimitedGenderNavigation { get; set; } = null!;

    public virtual ICollection<TclassSchedule> TclassSchedules { get; set; } = new List<TclassSchedule>();

    public virtual ICollection<TcoachExpert> TcoachExperts { get; set; } = new List<TcoachExpert>();
}
