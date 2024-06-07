using System;
using System.Collections.Generic;

namespace projRESTfulApiFitConnect.Models;

public partial class ClassReservedDetail
{
    public int ReserveId { get; set; }

    public int MemberId { get; set; }

    public bool PaymentStatus { get; set; }

    public bool ReserveStatus { get; set; }

    public int CoachId { get; set; }

    public int FieldId { get; set; }

    public DateOnly CourseDate { get; set; }

    public int? CourseStartTimeId { get; set; }

    public int? CourseEndTimeId { get; set; }

    public int MaxStudent { get; set; }

    public decimal ClassPayment { get; set; }

    public string ClassName { get; set; } = null!;

    public string? ClassIntroduction { get; set; }
}
