using System;

namespace projRESTfulApiFitConnect.DTO
{
    public class CoachDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string? EMail { get; set; }
        public string? Photo { get; set; }
        public DateTime Birthday { get; set; }
        public string Address { get; set; } = null!;
        public string RoleDescription { get; set; } = null!;
        public string GenderDescription { get; set; } = null!;
    }
}
