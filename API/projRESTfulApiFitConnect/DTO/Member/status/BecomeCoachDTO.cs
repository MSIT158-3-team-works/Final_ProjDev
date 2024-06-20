using projRESTfulApiFitConnect.DTO.Coach;
using projRESTfulApiFitConnect.DTO.Product;

namespace projRESTfulApiFitConnect.DTO.Member.status
{
    public class BecomeCoachDTO
    {
        public string? coachName { get; set; }
        public string? photo { get; set; }
        public string? ImageBase64 { get; set; }
        public List<int>? expert { get; set; }
        public string? intro { get; set; }
        public List<CoachImagesDTO>? Images { get; set; }
        public List<string>? moreBase64Images { get; set; }
    }

}
