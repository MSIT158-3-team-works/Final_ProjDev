using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using projRESTfulApiFitConnect.DTO.Coach;
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
        //取得所有已開課資料
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
                             .ThenInclude(te=>te.ClassSort2)
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
                    CoachId= item.CoachId,
                    ClassSort2Id = item.Class.ClassSort2Id,
                    ClassSort1Id = item.Class.ClassSort1Id,
                    Sort2Name = item.Class.ClassSort2.ClassSort2Detail,
                    Introduction = item.Class.ClassIntroduction,
                    Gym = item.Field.Gym.GymName,
                    GymId = item.Field.Gym.GymId,
                    Photo = base64Image,
                    CourseDate = item.CourseDate,
                    CourseStartTime = item.CourseStartTime.TimeName,
                    CourseEndTime = item.CourseEndTime.TimeName,
                    MaxStudent = item.MaxStudent,
                    ClassStatusId=item.ClassStatusId,
                    ClassStatus = item.ClassStatus.ClassStatusDiscribe,
                    ClassPayment = item.ClassPayment,
                    CoachPayment = item.CoachPayment
                };
                openCourseDtos.Add(openCourseDto);
            }

            return openCourseDtos;
        }
        //取得所有開課資料
        [HttpGet]
        [Route("ALL")]
        public async Task<ActionResult<IEnumerable<OpenCourseDto>>> GetAllCourses()
        {
            List<OpenCourseDto> openCourseDtos = await loadAllCourse();
            return Ok(openCourseDtos);
        }

        private async Task<List<OpenCourseDto>> loadAllCourse()
        {
            string filepath = "";
            List<OpenCourseDto> openCourseDtos = new List<OpenCourseDto>();
            var openCourses = await _context.TclassSchedules
                             .Include(x => x.ClassStatus)
                             .Include(x => x.Class)
                             .ThenInclude(te => te.ClassSort2)
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
                    CoachId = item.CoachId,
                    ClassSort2Id = item.Class.ClassSort2Id,
                    ClassSort1Id = item.Class.ClassSort1Id,
                    Sort2Name = item.Class.ClassSort2.ClassSort2Detail,
                    Introduction = item.Class.ClassIntroduction,
                    Gym = item.Field.Gym.GymName,
                    GymId = item.Field.Gym.GymId,
                    Photo = base64Image,
                    CourseDate = item.CourseDate,
                    CourseStartTime = item.CourseStartTime.TimeName,
                    CourseEndTime = item.CourseEndTime.TimeName,
                    MaxStudent = item.MaxStudent,
                    ClassStatusId = item.ClassStatusId,
                    ClassStatus = item.ClassStatus.ClassStatusDiscribe,
                    ClassPayment = item.ClassPayment,
                    CoachPayment = item.CoachPayment
                };
                openCourseDtos.Add(openCourseDto);
            }

            return openCourseDtos;
        }
        //取得待審核開課資料
        [HttpGet]
        [Route("APPROVAL")]
        public async Task<ActionResult<IEnumerable<OpenCourseDto>>> GetApprovalCourses()
        {
            List<OpenCourseDto> openCourseDtos = await loadApprovalCourse();
            return Ok(openCourseDtos);
        }

        private async Task<List<OpenCourseDto>> loadApprovalCourse()
        {
            string filepath = "";
            List<OpenCourseDto> openCourseDtos = new List<OpenCourseDto>();
            var openCourses = await _context.TclassSchedules
                             .Where(x => x.ClassStatusId == 4)
                             .Include(x => x.ClassStatus)
                             .Include(x => x.Class)
                             .ThenInclude(te => te.ClassSort2)
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
                    CoachId = item.CoachId,
                    ClassSort2Id = item.Class.ClassSort2Id,
                    ClassSort1Id = item.Class.ClassSort1Id,
                    Sort2Name = item.Class.ClassSort2.ClassSort2Detail,
                    Introduction = item.Class.ClassIntroduction,
                    Gym = item.Field.Gym.GymName,
                    GymId = item.Field.Gym.GymId,
                    Photo = base64Image,
                    CourseDate = item.CourseDate,
                    CourseStartTime = item.CourseStartTime.TimeName,
                    CourseEndTime = item.CourseEndTime.TimeName,
                    MaxStudent = item.MaxStudent,
                    ClassStatusId = item.ClassStatusId,
                    ClassStatus = item.ClassStatus.ClassStatusDiscribe,
                    ClassPayment = item.ClassPayment,
                    CoachPayment = item.CoachPayment
                };
                openCourseDtos.Add(openCourseDto);
            }

            return openCourseDtos;
        }
        private async Task<string> GetBase64Image(string courseImage)
        {
            string filepath = Path.Combine(_env.ContentRootPath, "Images", "ClassPic", courseImage);
            if (System.IO.File.Exists(filepath))
            {
                byte[] bytes = await System.IO.File.ReadAllBytesAsync(filepath);
                return Convert.ToBase64String(bytes);
            }
            return string.Empty;
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
                             .Include(x => x.Class).ThenInclude(te => te.ClassSort2)
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
                CoachId = openCourses.CoachId,
                ClassSort2Id = openCourses.Class.ClassSort2Id,
                Sort2Name = openCourses.Class.ClassSort2.ClassSort2Detail,
                Introduction = openCourses.Class.ClassIntroduction,
                Gym = openCourses.Field.Gym.GymName,
                GymId = openCourses.Field.Gym.GymId,
                fieldId = openCourses.Field.FieldId,
                Photo = base64Image,
                CourseDate = openCourses.CourseDate,
                CourseStartTime = openCourses.CourseStartTime.TimeName,
                CourseEndTime = openCourses.CourseEndTime.TimeName,
                MaxStudent = openCourses.MaxStudent,
                ClassStatusId = openCourses.ClassStatusId,
                ClassStatus = openCourses.ClassStatus.ClassStatusDiscribe,
                ClassPayment = openCourses.ClassPayment,
                CoachPayment = openCourses.CoachPayment,
                Images = images,
                Base64Images = new List<string>()
            };
            if (images.Count > 0)
            {
                foreach (var image in images)
                {
                    if (!string.IsNullOrEmpty(image.courseImages))
                    {
                        string base64Img = await GetBase64Image(image.courseImages);
                        openCourseDto.Base64Images.Add(base64Img);
                    }
                }
            }
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
            //根據課程狀態搜尋課程資料
            everyCourse = courseSearchDTO.ClassStatusId == 0 ? everyCourse : everyCourse.Where(s => s.ClassStatusId == courseSearchDTO.ClassStatusId);
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
                //依日期
                case "Date":
                    everyCourse = courseSearchDTO.sortType == "asc" ? everyCourse.OrderBy(s => s.CourseDate) : everyCourse.OrderByDescending(s => s.CourseDate);
                    break;
                //依課程價格
                case "CoursePrice":
                    everyCourse = courseSearchDTO.sortType == "asc" ? everyCourse.OrderBy(s => s.ClassPayment) : everyCourse.OrderByDescending(s => s.ClassPayment);
                    break;
                //依場館
                case "GymId":
                    everyCourse = courseSearchDTO.sortType == "asc" ? everyCourse.OrderBy(s => s.GymId) : everyCourse.OrderByDescending(s => s.GymId);
                    break;
                //依類別
                case "Sort":
                    everyCourse = courseSearchDTO.sortType == "asc" ? everyCourse.OrderBy(s => s.ClassSort2Id) : everyCourse.OrderByDescending(s => s.ClassSort2Id);
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
        [HttpPost]
        [Route("SEARCHALL")]
        public async Task<ActionResult<CoursePagingDTO>> GetAllCourseSearch(/*[FromQuery]*/ CourseSearchDTO courseSearchDTO)
        {
            List<OpenCourseDto> openCourseDtos = await loadAllCourse();

            //根據分類編號搜尋課程資料
            var everyCourse = courseSearchDTO.sort1 == 0 ? openCourseDtos : openCourseDtos.Where(s => s.ClassSort1Id == courseSearchDTO.sort1);
            everyCourse = courseSearchDTO.sort2 == 0 ? everyCourse : everyCourse.Where(s => s.ClassSort2Id == courseSearchDTO.sort2);
            //根據課程狀態搜尋課程資料
            everyCourse = courseSearchDTO.ClassStatusId == 0 ? everyCourse : everyCourse.Where(s => s.ClassStatusId == courseSearchDTO.ClassStatusId);
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
                //依日期
                case "Date":
                    everyCourse = courseSearchDTO.sortType == "asc" ? everyCourse.OrderBy(s => s.CourseDate) : everyCourse.OrderByDescending(s => s.CourseDate);
                    break;
                //依課程價格
                case "CoursePrice":
                    everyCourse = courseSearchDTO.sortType == "asc" ? everyCourse.OrderBy(s => s.ClassPayment) : everyCourse.OrderByDescending(s => s.ClassPayment);
                    break;
                //依場館
                case "GymId":
                    everyCourse = courseSearchDTO.sortType == "asc" ? everyCourse.OrderBy(s => s.GymId) : everyCourse.OrderByDescending(s => s.GymId);
                    break;
                //依類別
                case "Sort":
                    everyCourse = courseSearchDTO.sortType == "asc" ? everyCourse.OrderBy(s => s.ClassSort2Id) : everyCourse.OrderByDescending(s => s.ClassSort2Id);
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
        [HttpPost]
        [Route("APPROVAL")]
        public async Task<ActionResult<CoursePagingDTO>> GetApprovalCourseSearch(/*[FromQuery]*/ CourseSearchDTO courseSearchDTO)
        {
            List<OpenCourseDto> openCourseDtos = await loadApprovalCourse();

            //根據分類編號搜尋課程資料
            var everyCourse = courseSearchDTO.sort1 == 0 ? openCourseDtos : openCourseDtos.Where(s => s.ClassSort1Id == courseSearchDTO.sort1);
            everyCourse = courseSearchDTO.sort2 == 0 ? everyCourse : everyCourse.Where(s => s.ClassSort2Id == courseSearchDTO.sort2);
            //根據課程狀態搜尋課程資料
            everyCourse = courseSearchDTO.ClassStatusId == 0 ? everyCourse : everyCourse.Where(s => s.ClassStatusId == courseSearchDTO.ClassStatusId);
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
                //依日期
                case "Date":
                    everyCourse = courseSearchDTO.sortType == "asc" ? everyCourse.OrderBy(s => s.CourseDate) : everyCourse.OrderByDescending(s => s.CourseDate);
                    break;
                //依課程價格
                case "CoursePrice":
                    everyCourse = courseSearchDTO.sortType == "asc" ? everyCourse.OrderBy(s => s.ClassPayment) : everyCourse.OrderByDescending(s => s.ClassPayment);
                    break;
                //依場館
                case "GymId":
                    everyCourse = courseSearchDTO.sortType == "asc" ? everyCourse.OrderBy(s => s.GymId) : everyCourse.OrderByDescending(s => s.GymId);
                    break;
                //依類別
                case "Sort":
                    everyCourse = courseSearchDTO.sortType == "asc" ? everyCourse.OrderBy(s => s.ClassSort2Id) : everyCourse.OrderByDescending(s => s.ClassSort2Id);
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
        public async Task<IActionResult> UpdateClassScheduleStatus(int id, int statusId)
        {
            var classSchedule = await _context.TclassSchedules
                .FirstOrDefaultAsync(x => x.ClassScheduleId == id);

            if (classSchedule == null)
            {
                return NotFound("Class schedule not found");
            }

            classSchedule.ClassStatusId = statusId;

            _context.Entry(classSchedule).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok("Class schedule status has been successfully updated");
        }


        // DELETE: api/Course/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> ApprovalTclassSchedule(int id)
        {
            var tclassSchedule = await _context.TclassSchedules.FindAsync(id);
            if (tclassSchedule == null)
            {
                return NotFound();
            }

            _context.TclassSchedules.Remove(tclassSchedule);
            await _context.SaveChangesAsync();

            return Ok("已駁回");
        }

        private bool TclassScheduleExists(int id)
        {
            return _context.TclassSchedules.Any(e => e.ClassScheduleId == id);
        }
    }

}

