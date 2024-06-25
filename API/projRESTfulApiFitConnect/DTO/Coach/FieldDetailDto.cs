namespace projRESTfulApiFitConnect.DTO.Coach
{
    public class FieldDetailDto
    {
        public int FieldReserveId { get; set; }
        public int GymId { get; set; }
        public string? City { get; set; }
        public string? Region { get; set; }
        public string? GymName { get; set; }
        public string? GymTime { get; set; }
        public string? GymPhone { get; set; }
        public string? GymAddress { get; set; }
        public string GymPhoto { get; set; } = null!;

        public string? Field { get; set; }

        public double Payment { get; set; }
        public bool PaymentStatus { get; set; }

        public bool? ReserveStatus { get; set; }
    }
}
