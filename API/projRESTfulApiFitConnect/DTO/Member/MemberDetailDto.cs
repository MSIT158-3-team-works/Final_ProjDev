using projRESTfulApiFitConnect.DTO.Coach;

namespace projRESTfulApiFitConnect.DTO.Member
{
    public class MemberDetailDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string? EMail { get; set; }
        public string Photo { get; set; } = null!;
        public DateOnly Birthday { get; set; }
        public string Address { get; set; } = null!;
        public string? RoleDescription { get; set; } = null!;
        public string GenderDescription { get; set; } = null!;
        public int? GenderID { get; set; } = null!;

    }
}
