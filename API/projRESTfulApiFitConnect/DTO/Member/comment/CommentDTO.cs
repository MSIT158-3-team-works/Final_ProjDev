namespace projRESTfulApiFitConnect.DTO.Member.comment
{
    public class CommentDTO
    {
        public int ReserveId { get; set; }
        public int MemberId { get; set; }
        public decimal? RateClass { get; set; }
        public string? RateClassDescribe { get; set; }
        public decimal? RateCoach { get; set; }
        public string? RateCoachDescribe { get; set; }
    }
}
