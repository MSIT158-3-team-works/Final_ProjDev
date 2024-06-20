namespace projRESTfulApiFitConnect.DTO.Gym
{
    public class GymUpdateDto
    {
        public int? RegionId { get; set; }
        public string GymRegion { get; set; } = null!;
        public string GymName { get; set; } = null!;
        public string GymAddress { get; set; } = null!;
        public string GymPhone { get; set; } = null!;
        public string? GymPhoto { get; set; }
        public string? GymPark { get; set; }
        public string? GymTraffic { get; set; }
        public string? GymDescribe { get; set; }
        public string start_time { get; set; } = null!;
        public string end_time { get; set; } = null!;
        public string? ImageBase64 { get; set; }
    }
}
