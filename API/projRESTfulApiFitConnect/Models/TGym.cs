using System;
using System.Collections.Generic;

namespace projRESTfulApiFitConnect.Models;

public partial class TGym
{
    public int GymId { get; set; }

    public int CompanyId { get; set; }

    public int RegionId { get; set; }

    public string GymName { get; set; } = null!;

    public string GymAddress { get; set; } = null!;

    public string GymPhone { get; set; } = null!;

    public DateOnly ExpiryDate { get; set; }

    public string GymTime { get; set; } = null!;

    public string? GymPhoto { get; set; }

    public bool GymStatus { get; set; }

    public string? GymPark { get; set; }

    public string? GymTraffic { get; set; }

    public string? GymDescribe { get; set; }

    public virtual Tcompany Company { get; set; } = null!;

    public virtual TregionTable Region { get; set; } = null!;

    public virtual ICollection<TGymTime> TGymTimes { get; set; } = new List<TGymTime>();

    public virtual ICollection<Tfield> Tfields { get; set; } = new List<Tfield>();
}
