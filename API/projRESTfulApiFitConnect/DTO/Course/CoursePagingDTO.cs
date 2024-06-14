using projRESTfulApiFitConnect.DTO.Coach;

namespace projRESTfulApiFitConnect.DTO.Course
{
    public class CoursePagingDTO
    {
        public int TotalPages { get; set; }
        public int TotalCount { get; set; }
        public List<OpenCourseDto>? CourseResult { get; set; }
    }
}
