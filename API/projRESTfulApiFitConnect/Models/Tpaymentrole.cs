using System;
using System.Collections.Generic;

namespace projRESTfulApiFitConnect.Models;

public partial class Tpaymentrole
{
    public int PaymentroleId { get; set; }

    public string Paymentdetail { get; set; } = null!;

    public virtual ICollection<Tpayment> Tpayments { get; set; } = new List<Tpayment>();
}
