﻿namespace projRESTfulApiFitConnect.DTO.Course
{
    public class CourseSearchDTO
    {
        public int sort1 { get; set; } = 0;
        public int sort2 { get; set; } = 0;
        public string field { get; set; } = "";
        public DateOnly? CourseDate { get; set; }
        public TimeOnly? CourseStartTime { get; set; }
        public string? keyword { get; set; } = "";
        public int page { get; set; } = 1;
        public int pageSize { get; set; } = 8;
        public string? sortBy { get; set; } = "";
        public string? sortType { get; set; } = "asc";
    }
}
