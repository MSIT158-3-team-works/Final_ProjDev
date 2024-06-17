using System;
using System.Collections.Generic;

namespace projRESTfulApiFitConnect.Models;

public partial class Tfield
{
    public int FieldId { get; set; }

    public int GymId { get; set; }

    public string Floor { get; set; } = null!;

    public string FieldName { get; set; } = null!;

    public decimal FieldPayment { get; set; }

    public string? FieldDescribe { get; set; }

    public bool Status { get; set; }

    public virtual TGym Gym { get; set; } = null!;

    public virtual ICollection<TclassSchedule> TclassSchedules { get; set; } = new List<TclassSchedule>();

    public virtual ICollection<TfieldPhoto> TfieldPhotos { get; set; } = new List<TfieldPhoto>();

    public virtual ICollection<TfieldReserve> TfieldReserves { get; set; } = new List<TfieldReserve>();
}
