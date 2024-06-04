using System;
using System.Collections.Generic;

namespace projRESTfulApiFitConnect.Models;

public partial class TshippingMethod
{
    public int ShippingMethodId { get; set; }

    public string ShippingMethod { get; set; } = null!;

    public virtual ICollection<Torder> Torders { get; set; } = new List<Torder>();
}
