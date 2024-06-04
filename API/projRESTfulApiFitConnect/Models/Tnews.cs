using System;
using System.Collections.Generic;

namespace projRESTfulApiFitConnect.Models;

public partial class Tnews
{
    public int NewsId { get; set; }

    public int? NewsCategoryId { get; set; }

    public string NewsTitle { get; set; } = null!;

    public DateOnly NewsDate { get; set; }

    public string NewsContent { get; set; } = null!;

    public virtual TnewsCategory? NewsCategory { get; set; }

    public virtual ICollection<TnewsComment> TnewsComments { get; set; } = new List<TnewsComment>();

    public virtual ICollection<TnewsMedium> TnewsMedia { get; set; } = new List<TnewsMedium>();
}
