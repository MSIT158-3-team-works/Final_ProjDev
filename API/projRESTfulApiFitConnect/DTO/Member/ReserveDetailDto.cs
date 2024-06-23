namespace projRESTfulApiFitConnect.DTO.Member
{
    public class ReserveDetailDto
    {
        public int ReserveId { get; set; }
        public string? Class { get; set; }
        public int? Schedule_id { get; set; }
        public string? Coach { get; set; }
        public string? Address { get; set; }
        public string? Gym { get; set; }
        public string? Field { get; set; }
        public DateAndTimeDto? Time { get; set; }
        public int MaxStudent { get; set; }
        public decimal CourseFee { get; set; }
        public bool PaymentStatus { get; set; }
        public bool? ReserveStatus { get; set; }
    }
}
