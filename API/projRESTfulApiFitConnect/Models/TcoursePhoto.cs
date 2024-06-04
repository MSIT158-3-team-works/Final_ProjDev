using System;
using System.Collections.Generic;

namespace projRESTfulApiFitConnect.Models;

public partial class TcoursePhoto
{
    public int CoursePhotoId { get; set; }

    public int? ClassScheduleId { get; set; }

    public string? CoursePhoto { get; set; }

    public virtual TclassSchedule? ClassSchedule { get; set; }
}
