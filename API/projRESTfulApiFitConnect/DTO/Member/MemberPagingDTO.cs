using projRESTfulApiFitConnect.DTO.Coach;

namespace projRESTfulApiFitConnect.DTO.Member
{
    public class MemberPagingDTO
    {
        public int TotalPages { get; set; }
        public int TotalCount { get; set; }
        public List<MemberDetailDto>? MemberResult { get; set; }
    }
}
