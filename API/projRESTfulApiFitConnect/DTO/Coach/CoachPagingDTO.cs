namespace projRESTfulApiFitConnect.DTO.Coach
{
    public class CoachPagingDTO
    {
        public int TotalPages { get; set; }
        public int TotalCount { get; set; }
        public List<CoachDetailDto>? CoachResult { get; set; }
    }
}
