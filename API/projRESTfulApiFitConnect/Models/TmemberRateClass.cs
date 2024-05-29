using System;
using System.Collections.Generic;

namespace projRESTfulApiFitConnect.Models;

public partial class TmemberRateClass
{
    public int RateId { get; set; }

    public int ReserveId { get; set; }

    public int MemberId { get; set; }

    public int ClassId { get; set; }

    public int CoachId { get; set; }

    public decimal RateClass { get; set; }

    public string? ClassDescribe { get; set; }

    public decimal RateCoach { get; set; }

    public string? CoachDescribe { get; set; }

    public virtual TclassReserve Reserve { get; set; } = null!;
}
