namespace projRESTfulApiFitConnect.DTO.Gym
{
    public class GymReviewDetailDto
    {
        public int GymId { get; set; }
        public string Owner { get; set; } = null!;
        public int CompanyId { get; set; }
        public string Name { get; set; } = null!;

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
        public int CityId { get; set; }
        public string? City { get; set; }
        public string Region { get; set; } = null!;
        public string start_time { get; set; } = null!;
        public string end_time { get; set; } = null!;
    }
}
