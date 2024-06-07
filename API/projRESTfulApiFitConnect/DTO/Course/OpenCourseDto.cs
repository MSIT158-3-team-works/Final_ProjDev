namespace projRESTfulApiFitConnect.DTO.Course
{
    public class OpenCourseDto
    {
        public int ClassScheduleId { get; set; }

        public string? Class { get; set; }

        public string? Coach { get; set; }
        public string? Introduction { get; set; }
        public int? ClassSort2Id { get; set; }
        public int? GymId { get; set; }
        public string? Gym { get; set; }
        public string Photo { get; set; } = null!;
        public DateOnly CourseDate { get; set; }

        public TimeOnly CourseStartTime { get; set; }
        public TimeOnly CourseEndTime { get; set; }

        public int MaxStudent { get; set; }

        public string? ClassStatus { get; set; }

        public decimal ClassPayment { get; set; }

        public bool CoachPayment { get; set; }
    }
}
