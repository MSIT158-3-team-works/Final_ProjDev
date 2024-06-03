using projRESTfulApiFitConnect.Models;

namespace projRESTfulApiFitConnect.DTO.Coach
{
    public class PutCoachDto
    {
        public int Id { get; set; }

        public int RoleId { get; set; } = 2;

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
