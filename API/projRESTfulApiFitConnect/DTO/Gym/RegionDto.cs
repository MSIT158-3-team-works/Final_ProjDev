namespace projRESTfulApiFitConnect.DTO.Gym
{
    public class RegionDto
    {
        public int RegionId { get; set; }

        public int CityId { get; set; }

        public string City { get; set; } = null!;
        public string Region { get; set; } = null!;
    }
}
