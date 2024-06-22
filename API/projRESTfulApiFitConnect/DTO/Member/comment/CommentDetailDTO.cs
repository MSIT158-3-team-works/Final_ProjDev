using projRESTfulApiFitConnect.DTO.Coach;

namespace projRESTfulApiFitConnect.DTO.Member.comment
{
    public class CommentDetailDTO
    {
        public int ClassReserveId { get; set; }
        public string? ClassName { get; set; }
        public int? Coach { get; set; }
        public string? CoachName { get; set; }
        public string? GymName { get; set; }
        public DateOnly CourseDate { get; set; }
        public TimeOnly CourseStartTime { get; set; }
        public List<RatesDTO>? Rates { get; set; }

    }
}
