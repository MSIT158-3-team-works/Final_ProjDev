using projRESTfulApiFitConnect.DTO.Member.status;

namespace projRESTfulApiFitConnect.DTO.Member
{
    public class FollowAndBlackListDto
    {
        public List<C_memberfollow>? Follow { get; set; }
        public List<C_memberfollow>? BlackList { get; set; }
    }
}
