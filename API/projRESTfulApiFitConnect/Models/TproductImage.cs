using System;
using System.Collections.Generic;

namespace projRESTfulApiFitConnect.Models;

public partial class TproductImage
{
    public int ProductImagesId { get; set; }

    public int ProductId { get; set; }

    public string? ProductImages { get; set; }

    public virtual Tproduct Product { get; set; } = null!;
}
