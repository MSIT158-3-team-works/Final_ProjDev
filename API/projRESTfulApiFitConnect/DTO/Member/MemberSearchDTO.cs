namespace projRESTfulApiFitConnect.DTO.Member
{
    public class MemberSearchDTO
    {
        public int gender { get; set; } = 0;
        public string? keyword { get; set; }
        public int page { get; set; } = 1;
        public int pageSize { get; set; } = 20;
        public string? sortBy { get; set; }
        public string? sortType { get; set; } = "asc";
    }
}
