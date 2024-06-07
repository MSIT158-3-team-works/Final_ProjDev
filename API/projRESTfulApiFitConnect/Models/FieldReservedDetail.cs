using System;
using System.Collections.Generic;

namespace projRESTfulApiFitConnect.Models;

public partial class FieldReservedDetail
{
    public int FieldReserveId { get; set; }

    public int FieldId { get; set; }

    public int CoachId { get; set; }

    public DateOnly? FieldDate { get; set; }

    public int? FieldReserveStartTime { get; set; }

    public int? FieldReserveEndTime { get; set; }

    public string Floor { get; set; } = null!;

    public string FieldName { get; set; } = null!;

    public decimal FieldPayment { get; set; }

    public string? FieldDescribe { get; set; }

    public string GymName { get; set; } = null!;

    public string GymAddress { get; set; } = null!;

    public string? GymDescribe { get; set; }

    public string Region { get; set; } = null!;

    public string City { get; set; } = null!;
}
