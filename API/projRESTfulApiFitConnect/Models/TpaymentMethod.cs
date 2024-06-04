using System;
using System.Collections.Generic;

namespace projRESTfulApiFitConnect.Models;

public partial class TpaymentMethod
{
    public int PaymentMethodId { get; set; }

    public string PaymentMethod { get; set; } = null!;

    public virtual ICollection<Torder> Torders { get; set; } = new List<Torder>();
}
