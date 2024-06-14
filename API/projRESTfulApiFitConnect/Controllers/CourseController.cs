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
            List<OpenCourseDto> openCourseDtos = await loadCourse();
            return Ok(openCourseDtos);
        }

        private async Task<List<OpenCourseDto>> loadCourse()
        {
            string filepath = "";
            List<OpenCourseDto> openCourseDtos = new List<OpenCourseDto>();
            var openCourses = await _context.TclassSchedules
                             .Where(x => x.ClassStatusId == 2)
                             .Include(x => x.ClassStatus)
                             .Include(x => x.Class)
                             .Include(x => x.Coach)
                             .Include(x => x.Field).ThenInclude(te => te.Gym)
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
                    ClassSort2Id = item.Class.ClassSort2Id,
                    ClassSort1Id = item.Class.ClassSort1Id,
                    Introduction = item.Class.ClassIntroduction,
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

            return openCourseDtos;
        }

        // GET: api/Course
        //取得一堂開課資料
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<OpenCourseDto>>> GetCourses(int? id)
        {
            string filepath = "";
            List<OpenCourseDto> openCourseDtos = new List<OpenCourseDto>();
            var openCourses = await _context.TclassSchedules
                             .Where(x => x.ClassStatusId == 2)
                             .Where(x => x.ClassScheduleId == id)
                             .Include(x => x.ClassStatus)
                             .Include(x => x.Class)
                             .Include(x => x.Coach)
                             .Include(x => x.Field).ThenInclude(te => te.Gym)
                             .Include(x => x.CourseStartTime)
                             .Include(x => x.CourseEndTime)
                             .Include(x => x.TcoursePhotos)
                             .FirstOrDefaultAsync();

            string base64Image = "";
            filepath = Path.Combine(_env.ContentRootPath, "Images", "ClassPic", openCourses.Class.ClassPhoto);
            if (System.IO.File.Exists(filepath))
            {
                byte[] bytes = await System.IO.File.ReadAllBytesAsync(filepath);
                base64Image = Convert.ToBase64String(bytes);
            }
            var images = openCourses.TcoursePhotos.Select(img => new CourseImagesDTO
            {
                courseImages = img.CoursePhoto
            }).ToList();

            OpenCourseDto openCourseDto = new OpenCourseDto()
            {
                ClassScheduleId = openCourses.ClassScheduleId,
                Class = openCourses.Class.ClassName,
                Coach = openCourses.Coach.Name,
                ClassSort2Id = openCourses.Class.ClassSort2Id,
                Introduction = openCourses.Class.ClassIntroduction,
                Gym = openCourses.Field.Gym.GymName,
                GymId = openCourses.Field.Gym.GymId,
                fieldId = openCourses.Field.FieldId,
                Photo = base64Image,
                CourseDate = openCourses.CourseDate,
                CourseStartTime = openCourses.CourseStartTime.TimeName,
                CourseEndTime = openCourses.CourseEndTime.TimeName,
                MaxStudent = openCourses.MaxStudent,
                ClassStatus = openCourses.ClassStatus.ClassStatusDiscribe,
                ClassPayment = openCourses.ClassPayment,
                CoachPayment = openCourses.CoachPayment,
                Images = images
            };
            openCourseDtos.Add(openCourseDto);

            return Ok(openCourseDtos);
        }

        [HttpPost]
        [Route("SEARCH")]
        public async Task<ActionResult<CoursePagingDTO>> GetCourseSearch(/*[FromQuery]*/ CourseSearchDTO courseSearchDTO)
        {
            List<OpenCourseDto> openCourseDtos = await loadCourse();

            //根據分類編號搜尋課程資料
            var everyCourse = courseSearchDTO.sort1 == 0 ? openCourseDtos : openCourseDtos.Where(s => s.ClassSort1Id == courseSearchDTO.sort1);
            everyCourse = courseSearchDTO.sort2 == 0 ? everyCourse : everyCourse.Where(s => s.ClassSort2Id == courseSearchDTO.sort2);

            //根據Time分類搜尋課程資料            
            everyCourse = courseSearchDTO.CourseDate == null ? everyCourse : everyCourse.Where(s => s.CourseDate == courseSearchDTO.CourseDate);
            everyCourse = courseSearchDTO.CourseStartTime == null ? everyCourse : everyCourse.Where(s => s.CourseStartTime == courseSearchDTO.CourseStartTime);

            //根據關鍵字搜尋課程資料(姓名、性別、教練資訊、專長名稱)
            if (!string.IsNullOrEmpty(courseSearchDTO.keyword))
            {
                everyCourse = everyCourse.Where(s => s.Class.Contains(courseSearchDTO.keyword) ||
                 s.Introduction.Contains(courseSearchDTO.keyword) ||
                 s.Gym.Contains(courseSearchDTO.keyword));
            }
            //根據場館搜尋課程資料
            if (!string.IsNullOrEmpty(courseSearchDTO.field))
            {
                everyCourse = everyCourse.Where(s => s.Gym.Contains(courseSearchDTO.field));
            }

            //排序
            switch (courseSearchDTO.sortBy)
            {
                //依性別
                case "Date":
                    everyCourse = courseSearchDTO.sortType == "asc" ? everyCourse.OrderBy(s => s.CourseDate) : everyCourse.OrderByDescending(s => s.CourseDate);
                    break;
                //依性別
                case "GymId":
                    everyCourse = courseSearchDTO.sortType == "asc" ? everyCourse.OrderBy(s => s.GymId) : everyCourse.OrderByDescending(s => s.GymId);
                    break;
                //預設為id
                default:
                    everyCourse = courseSearchDTO.sortType == "asc" ? everyCourse.OrderBy(s => s.ClassScheduleId) : everyCourse.OrderByDescending(s => s.ClassScheduleId);
                    break;
            }
            //總共有多少筆資料
            int totalCount = everyCourse.Count();
            //每頁要顯示幾筆資料
            int pageSize = (int)courseSearchDTO.pageSize;
            //目前第幾頁
            int page = (int)courseSearchDTO.page;
            //計算總共有幾頁
            int totalPages = (int)Math.Ceiling((decimal)totalCount / pageSize);
            //分頁
            everyCourse = everyCourse.Skip((page - 1) * pageSize).Take(pageSize);
            //包裝要傳給client端的資料
            CoursePagingDTO coursePaging = new CoursePagingDTO();
            coursePaging.TotalCount = totalCount;
            coursePaging.TotalPages = totalPages;
            coursePaging.CourseResult = everyCourse.ToList();
            return Ok(coursePaging);
        }



        // POST: api/Course
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TclassSchedule>> PostTclassSchedule(TclassSchedule tclassSchedule)
        {
            _context.TclassSchedules.Add(tclassSchedule);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTclassSchedule", new { id = tclassSchedule.ClassScheduleId }, tclassSchedule);
        }

        // PUT: api/Course/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClassSchedule(PutCourseDTO putCourseDTO, IFormFile img)
        {
            var course = await _context.TclassSchedules.FindAsync(putCourseDTO.ClassScheduleId);

            await _context.SaveChangesAsync();

            return Ok("課程資訊已成功更新");
        }

        // DELETE: api/Course/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTclassSchedule(int id)
        {
            var tclassSchedule = await _context.TclassSchedules.FindAsync(id);
            if (tclassSchedule == null)
            {
                return NotFound();
            }

            _context.TclassSchedules.Remove(tclassSchedule);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TclassScheduleExists(int id)
        {
            return _context.TclassSchedules.Any(e => e.ClassScheduleId == id);
        }
    }

}

