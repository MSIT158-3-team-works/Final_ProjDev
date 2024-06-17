using projRESTfulApiFitConnect.DTO.Gym;
using projRESTfulApiFitConnect.DTO.Product;
using System;

namespace projRESTfulApiFitConnect.DTO.Coach
{
    public class CoachDetailDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string? EMail { get; set; }
        public string Photo { get; set; } = null!;
        public string? Intro { get; set; }
        public DateOnly Birthday { get; set; }
        public List<ExpertiseDto>? Experties { get; set; }
        public string Address { get; set; } = null!;
        public string? RoleDescription { get; set; } = null!;
        public int? GenderID { get; set; } = null!;
        public string? GenderDescription { get; set; } = null!;
        public List<CityDto>? Region { get; set; }
        public List<rateCoachDTO>? CoachRate { get; set; }
        public List<CoachImagesDTO>? Images { get; set; }
        public List<string>? Base64Images { get; set; }
    }
}
