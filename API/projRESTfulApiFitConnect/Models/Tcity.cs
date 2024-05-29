using System;
using System.Collections.Generic;

namespace projRESTfulApiFitConnect.Models;

public partial class Tcity
{
    public int CityId { get; set; }

    public string City { get; set; } = null!;

    public virtual ICollection<TregionTable> TregionTables { get; set; } = new List<TregionTable>();
}
