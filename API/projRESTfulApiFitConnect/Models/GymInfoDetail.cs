using System;
using System.Collections.Generic;

namespace projRESTfulApiFitConnect.Models;

public partial class GymInfoDetail
{
    public int CityId { get; set; }

    public string City { get; set; } = null!;

    public int RegionId { get; set; }

    public string Region { get; set; } = null!;

    public int GymId { get; set; }

    public string GymName { get; set; } = null!;

    public string GymAddress { get; set; } = null!;

    public string GymTime { get; set; } = null!;

    public string? GymPhoto { get; set; }

    public string? GymPark { get; set; }

    public string? GymTraffic { get; set; }

    public string? GymDescribe { get; set; }

    public bool GymStatus { get; set; }
}
