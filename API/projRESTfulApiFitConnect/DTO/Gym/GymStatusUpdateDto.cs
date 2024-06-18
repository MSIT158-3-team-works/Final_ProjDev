namespace projRESTfulApiFitConnect.DTO.Gym
{
    public class GymStatusUpdateDto
    {
        public int GymId { get; set; }
        public bool GymStatus { get; set; }
        public string start_time { get; set; } = null!;
        public string end_time { get; set; } = null!;
        public bool Status { get; set; }

    }
}
