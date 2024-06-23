using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using projRESTfulApiFitConnect.DTO.Schdule;
using projRESTfulApiFitConnect.Models;

namespace projRESTfulApiFitConnect.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchduleController : ControllerBase
    {
        private readonly GymContext _context;
        private readonly IWebHostEnvironment _env;

        public SchduleController(GymContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        //新增課程
        [HttpPost]
        public async Task<IActionResult> CreateSchdule([FromForm] SchduleDto schduleDto)
        {
            TclassSchedule tclassSchedule = new TclassSchedule
            {
                ClassId = schduleDto.courseId,
                CoachId = schduleDto.coachId,
                FieldId = schduleDto.fieldId,
                CourseDate = DateOnly.FromDateTime(schduleDto.date),
                CourseTimeId = schduleDto.startTimeId,
                CourseStartTimeId = schduleDto.startTimeId,
                CourseEndTimeId = schduleDto.endTimeid,
                MaxStudent = schduleDto.maxStudent,
                ClassStatusId = 4, //審核中
                ClassPayment = schduleDto.classPayment,
                CoachPayment = false
            };
            TfieldReserve tfieldReserve = new TfieldReserve
            {
                CoachId = schduleDto.coachId,
                FieldId = schduleDto.fieldId,
                FieldDate = DateOnly.FromDateTime(schduleDto.date),
                FieldReserveStartTime = schduleDto.startTimeId,
                FieldReserveEndTime = schduleDto.endTimeid,
                PaymentStatus = false,
                ReserveStatus = true
            };

            _context.TclassSchedules.Add(tclassSchedule);
            _context.TfieldReserves.Add(tfieldReserve);
            _context.SaveChanges();
            return Ok(new {success = "success"});
        }

    }
}
