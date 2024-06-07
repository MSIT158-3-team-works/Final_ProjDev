using System;
using System.Collections.Generic;

namespace projRESTfulApiFitConnect.Models;

public partial class TorderDetail
{
    public int OrderDetailId { get; set; }

    public int OrderId { get; set; }

    public int ProductId { get; set; }

    public int OrderQuantity { get; set; }

    public decimal? ProductUnitprice { get; set; }

    public int? DiscountId { get; set; }

    public virtual Tdiscount? Discount { get; set; }

    public virtual Torder Order { get; set; } = null!;

    public virtual Tproduct Product { get; set; } = null!;
}
