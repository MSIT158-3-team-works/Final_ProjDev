namespace projRESTfulApiFitConnect.DTO
{
    public class GymListDto
    {
        public int GymId { get; set; }
        public int RegionId { get; set; }
        public string GymName { get; set; } = null!;

        public string GymAddress { get; set; } = null!;

        public string GymPhone { get; set; } = null!;
        public string GymTime { get; set; } = null!;

        public string? GymPhoto { get; set; }
        public string? GymPark { get; set; }
        public string? GymTraffic { get; set; }
        public string? GymDescribe { get; set; }
        public int CityId { get; set; }

        public string Region { get; set; } = null!;
        public string? FieldDescribe { get; set; } = null!;
    }
}
