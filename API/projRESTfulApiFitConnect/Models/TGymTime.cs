using System;
using System.Collections.Generic;

namespace projRESTfulApiFitConnect.Models;

public partial class TGymTime
{
    public int TGymTimeId { get; set; }

    public int GymId { get; set; }

    public int GymTime { get; set; }

    public virtual TGym Gym { get; set; } = null!;

    public virtual ICollection<TfieldReserve> TfieldReserveFieldReserveEndTimeNavigations { get; set; } = new List<TfieldReserve>();

    public virtual ICollection<TfieldReserve> TfieldReserveFieldReserveStartTimeNavigations { get; set; } = new List<TfieldReserve>();
}
