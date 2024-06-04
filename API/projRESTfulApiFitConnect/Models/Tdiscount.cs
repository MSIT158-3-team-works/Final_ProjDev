using System;
using System.Collections.Generic;

namespace projRESTfulApiFitConnect.Models;

public partial class Tdiscount
{
    public int DiscountId { get; set; }

    public string DiscountCode { get; set; } = null!;

    public string? DiscountCondition { get; set; }

    public string DiscountValue { get; set; } = null!;

    public string? DiscountExpire { get; set; }

    public bool DiscountOpened { get; set; }

    public virtual ICollection<TorderDetail> TorderDetails { get; set; } = new List<TorderDetail>();
}
