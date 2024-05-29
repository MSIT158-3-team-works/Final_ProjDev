using System;
using System.Collections.Generic;

namespace projRESTfulApiFitConnect.Models;

public partial class Towner
{
    public int OwnerId { get; set; }

    public string Owner { get; set; } = null!;

    public virtual ICollection<Tcompany> Tcompanies { get; set; } = new List<Tcompany>();
}
