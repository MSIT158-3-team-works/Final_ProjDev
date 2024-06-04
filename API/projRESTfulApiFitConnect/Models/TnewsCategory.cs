using System;
using System.Collections.Generic;

namespace projRESTfulApiFitConnect.Models;

public partial class TnewsCategory
{
    public int NewsCategoryId { get; set; }

    public string NewsCategory { get; set; } = null!;

    public virtual ICollection<Tnews> Tnews { get; set; } = new List<Tnews>();
}
