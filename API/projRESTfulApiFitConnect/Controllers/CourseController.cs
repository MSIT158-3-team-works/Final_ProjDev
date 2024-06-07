using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using projRESTfulApiFitConnect.DTO.Course;
using projRESTfulApiFitConnect.Models;

namespace projRESTfulApiFitConnect.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly GymContext _context;
        private readonly IWebHostEnvironment _env;

        public CourseController(GymContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // GET: api/Course
        //取得所有開課資料
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OpenCourseDto>>> GetCourses() 
        {
            string filepath = "";
            List<OpenCourseDto>openCourseDtos = new List<OpenCourseDto>();
            var openCourses = await _context.TclassSchedules
                             .Where(x => x.ClassStatusId == 2)
                             .Include(x=>x.ClassStatus)
                             .Include(x=>x.Class)
                             .Include(x=>x.Coach)
                             .Include(x=>x.Field).ThenInclude(te=>te.Gym)
                             .Include(x => x.CourseStartTime)
                             .Include(x => x.CourseEndTime)
                             .ToListAsync();
            foreach (var item in openCourses) 
            {
                string base64Image = "";
                filepath = Path.Combine(_env.ContentRootPath, "Images", "ClassPic", item.Class.ClassPhoto);
                if (System.IO.File.Exists(filepath))
                {
                    byte[] bytes = await System.IO.File.ReadAllBytesAsync(filepath);
                    base64Image = Convert.ToBase64String(bytes);
                }
                OpenCourseDto openCourseDto = new OpenCourseDto()
                {
                    ClassScheduleId = item.ClassScheduleId,
                    Class = item.Class.ClassName,
                    Coach = item.Coach.Name,
                    ClassSort2Id=item.Class.ClassSort2Id,
                    Introduction =item.Class.ClassIntroduction,
                    Gym = item.Field.Gym.GymName,
                    GymId = item.Field.Gym.GymId,
                    Photo = base64Image,
                    CourseDate = item.CourseDate,
                    CourseStartTime = item.CourseStartTime.TimeName,
                    CourseEndTime = item.CourseEndTime.TimeName,
                    MaxStudent = item.MaxStudent,
                    ClassStatus = item.ClassStatus.ClassStatusDiscribe,
                    ClassPayment = item.ClassPayment,
                    CoachPayment = item.CoachPayment
                };
                openCourseDtos.Add(openCourseDto);
            }
            return Ok(openCourseDtos);
        }
    }
   
}

