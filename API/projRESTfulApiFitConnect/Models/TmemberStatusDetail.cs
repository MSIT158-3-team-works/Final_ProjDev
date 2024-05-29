using System;
using System.Collections.Generic;

namespace projRESTfulApiFitConnect.Models;

public partial class TmemberStatusDetail
{
    public int StatusId { get; set; }

    public string StatusDescribe { get; set; } = null!;

    public virtual ICollection<TmemberFollow> TmemberFollows { get; set; } = new List<TmemberFollow>();
}
