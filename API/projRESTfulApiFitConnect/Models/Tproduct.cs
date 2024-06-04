using System;
using System.Collections.Generic;

namespace projRESTfulApiFitConnect.Models;

public partial class Tproduct
{
    public int ProductId { get; set; }

    public string ProductName { get; set; } = null!;

    public int? CategoryId { get; set; }

    public decimal ProductUnitprice { get; set; }

    public string? ProductDetail { get; set; }

    public string? ProductImage { get; set; }

    public bool ProductSupplied { get; set; }

    public virtual TproductCategory? Category { get; set; }

    public virtual ICollection<TorderDetail> TorderDetails { get; set; } = new List<TorderDetail>();

    public virtual ICollection<TproductImage> TproductImages { get; set; } = new List<TproductImage>();

    public virtual ICollection<TproductShoppingcart> TproductShoppingcarts { get; set; } = new List<TproductShoppingcart>();

    public virtual ICollection<TproductTrack> TproductTracks { get; set; } = new List<TproductTrack>();
}
