namespace projRESTfulApiFitConnect.DTO.Coach
{
    public class FieldDetailDto
    {
        public int FieldReserveId { get; set; }
        public string? City { get; set; }
        public string? Region { get; set; }
        public string? Gym { get; set; }

        public string? Field { get; set; }

        public bool PaymentStatus { get; set; }

        public bool? ReserveStatus { get; set; }
    }
}
