namespace projRESTfulApiFitConnect.DTO.Member
{
    public class PutMemberDto
    {
        public int Id { get; set; } = 0;

        public int RoleId { get; set; } = 1;

        public string Name { get; set; } = null!;

        public string Phone { get; set; } = null!;

        public string? EMail { get; set; }

        public string Password { get; set; } = null!;
        public IFormFile Photo { get; set; }

        public DateTime Birthday { get; set; } = DateTime.Now;

        public string Address { get; set; } = null!;

        public int GenderId { get; set; } = 1;
    }
}
