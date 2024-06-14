using System;
using System.Collections.Generic;

namespace projRESTfulApiFitConnect.Models;

public partial class Tcompany
{
    public int CompanyId { get; set; }

    public int OwnerId { get; set; }

    public string Name { get; set; } = null!;

    public DateOnly Timelimit { get; set; }

    public bool Status { get; set; }

    public virtual Towner Owner { get; set; } = null!;

    public virtual ICollection<TGym> TGyms { get; set; } = new List<TGym>();
}
