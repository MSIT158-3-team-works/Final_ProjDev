namespace projRESTfulApiFitConnect.DTO.Coach
{
    public class CoachSearchDTO
    {
        public string? sort1 { get; set; }
        public string? sort2 { get; set; }
        public string? gender { get; set; }
        public string? city { get; set; } = "";
        public string? region { get; set; } = "";
        public int classId { get; set; } = 0;
        public string? keyword { get; set; }
        public int page { get; set; } = 1;
        public int pageSize { get; set; } = 8;
        public string? sortBy { get; set; }
        public string? sortType { get; set; } = "asc";
    }
}
