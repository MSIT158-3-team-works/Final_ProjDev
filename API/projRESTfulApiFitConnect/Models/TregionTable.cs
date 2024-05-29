using System;
using System.Collections.Generic;

namespace projRESTfulApiFitConnect.Models;

public partial class TregionTable
{
    public int RegionId { get; set; }

    public int CityId { get; set; }

    public string Region { get; set; } = null!;

    public virtual Tcity City { get; set; } = null!;

    public virtual ICollection<TGym> TGyms { get; set; } = new List<TGym>();
}
