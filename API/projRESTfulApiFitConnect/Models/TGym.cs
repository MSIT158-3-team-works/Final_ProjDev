using System;
using System.Collections.Generic;

namespace projRESTfulApiFitConnect.Models;

public partial class TGym
{
    public int GymId { get; set; }

    public int CompanyId { get; set; }

    public int RegionId { get; set; }

    public string Name { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string? EMail { get; set; }

    public string Phone { get; set; } = null!;

    public string? Website { get; set; }

    public string Time { get; set; } = null!;

    public string? Photo { get; set; }

    public bool Status { get; set; }

    public string? Describe { get; set; }

    public virtual Tcompany Company { get; set; } = null!;

    public virtual TregionTable Region { get; set; } = null!;

    public virtual ICollection<Tfield> Tfields { get; set; } = new List<Tfield>();
}
