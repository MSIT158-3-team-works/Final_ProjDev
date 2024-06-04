using System;
using System.Collections.Generic;

namespace projRESTfulApiFitConnect.Models;

public partial class Tpayment
{
    public int PaymentId { get; set; }

    public int PaymentroleId { get; set; }

    public int PersonId { get; set; }

    public decimal Payment { get; set; }

    public DateOnly Date { get; set; }

    public virtual Tpaymentrole Paymentrole { get; set; } = null!;
}
