namespace projRESTfulApiFitConnect.DTO.Member.comment
{
    public class RatesDTO
    {
        public int RateId {  get; set; }
        public int ReserveId { get; set; }
        public int MemberId { get; set; }
        public int ClassId { get; set; }
        public int CoachId { get; set; }
        public string? CoachName { get; set; }
        public decimal? RateClass { get; set; }
        public string? RateClassDescribe { get; set; }
        public decimal? RateCoach { get; set; }
        public string? RateCoachDescribe { get; set; }
    }
}
