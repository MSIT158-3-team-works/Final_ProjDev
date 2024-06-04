using System;
using System.Collections.Generic;

namespace projRESTfulApiFitConnect.Models;

public partial class TorderStatus
{
    public int OrderStatusId { get; set; }

    public string OrderStatus { get; set; } = null!;

    public virtual ICollection<Torder> Torders { get; set; } = new List<Torder>();
}
