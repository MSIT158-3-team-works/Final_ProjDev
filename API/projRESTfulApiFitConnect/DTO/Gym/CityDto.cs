namespace projRESTfulApiFitConnect.DTO.Gym
{
    public class CityDto
    {
        public int GymId { get; set; }
        public int RegionId { get; set; }

        public int CityId { get; set; }

        public string City { get; set; } = null!;
        public string Region { get; set; } = null!;
    }
}
