using System;
using System.Collections.Generic;

namespace projRESTfulApiFitConnect.Models;

public partial class TproductShoppingcart
{
    public int ProductShoppingcartId { get; set; }

    public int ProductId { get; set; }

    public int MemberId { get; set; }

    public int ProductQuantity { get; set; }

    public decimal? ProductUnitprice { get; set; }

    public virtual TIdentity Member { get; set; } = null!;

    public virtual Tproduct Product { get; set; } = null!;
}
