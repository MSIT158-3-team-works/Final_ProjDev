namespace projRESTfulApiFitConnect.DTO.Member
{
    public class PutMemberDto
    {
        public string Name { get; set; } = null!;

        public string Phone { get; set; } = null!;

        public string? EMail { get; set; }

        public string? Photo { get; set; }
        public string? ImageBase64 { get; set; }
        public string Address { get; set; } = null!;

    }
}
