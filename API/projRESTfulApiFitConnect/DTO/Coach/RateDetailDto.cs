namespace projRESTfulApiFitConnect.DTO.Coach
{
    public class RateDetailDto
    {
        public int ReserveId { get; set; }

        public string? Member { get; set; }

        public string? Coach { get; set; }

        public string? Class { get; set; }

        public decimal RateClass { get; set; }

        public string? ClassDescribe { get; set; }

        public decimal RateCoach { get; set; }

        public string? CoachDescribe { get; set; }

    }
}
