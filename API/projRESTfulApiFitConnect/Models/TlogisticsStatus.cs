using System;
using System.Collections.Generic;

namespace projRESTfulApiFitConnect.Models;

public partial class TlogisticsStatus
{
    public int LogisticsStatusId { get; set; }

    public string LogisticsStatus { get; set; } = null!;

    public virtual ICollection<Torder> Torders { get; set; } = new List<Torder>();
}
