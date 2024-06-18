namespace projRESTfulApiFitConnect.DTO.Gym
{
    public class FieldReviewDetailDto
    {
        public int FieldId { get; set; }
        public int GymId { get; set; }
        public string GymName { get; set; } = null!;
        public string? FieldName { get; set; } = null!;
        public string? Floor { get; set; } = null!;
        public string? FieldPhoto { get; set; } = null!;
        public string? FieldDescribe { get; set; } = null!;
        public decimal FieldPayment { get; set; }
        public bool Status { get; set; }
    }
}
