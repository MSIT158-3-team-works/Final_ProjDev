using System;
using System.Collections.Generic;

namespace projRESTfulApiFitConnect.Models;

public partial class TproductCategory
{
    public int ProductCategoryId { get; set; }

    public string CategoryName { get; set; } = null!;

    public string? CategoryDescription { get; set; }

    public string? CategoryImage { get; set; }

    public virtual ICollection<Tproduct> Tproducts { get; set; } = new List<Tproduct>();
}
