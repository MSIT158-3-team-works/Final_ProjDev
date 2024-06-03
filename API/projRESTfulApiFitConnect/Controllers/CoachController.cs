using System;
using System.Collections.Generic;
using System.Composition;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using projRESTfulApiFitConnect.DTO.Coach;
using projRESTfulApiFitConnect.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace projRESTfulApiFitConnect.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoachController : ControllerBase
    {
        private readonly GymContext _context;
        private readonly IWebHostEnvironment _env;

        public CoachController(GymContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // GET: api/Coach
        //取得所有教練資料(個人資料、自我介紹)
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CoachDetailDto>>> GetCoaches()
        {
            string filepath = "";

            List<CoachDetailDto> coachDetailDtos = new List<CoachDetailDto>();

            var coaches = await _context.TIdentities
                        .Where(x => x.RoleId == 2)
                        .Include(x => x.Gender)
                        .Include(x => x.TcoachInfoIds)
                        .Include(x => x.TcoachExperts)
                        .ThenInclude(te => te.Class)
                        .ToListAsync();

            foreach (var item in coaches)
            {
                var experts = item.TcoachExperts.Select(te => new ExpertiseDto
                {
                    ClassName = te.Class.ClassName
                }).ToList();
                string base64Image = "";
                filepath = Path.Combine(_env.ContentRootPath, "Images", "CoachImages", item.Photo);
                if (System.IO.File.Exists(filepath))
                {
                    byte[] bytes = await System.IO.File.ReadAllBytesAsync(filepath);
                    base64Image = Convert.ToBase64String(bytes);
                }
                string intro = string.Join(", ", item.TcoachInfoIds.Select(i => i.CoachIntro));

                CoachDetailDto coachDetailDto = new CoachDetailDto()
                {
                    Id = item.Id,
                    Name = item.Name,
                    Phone = item.Phone,
                    EMail = item.EMail,
                    Photo = base64Image,
                    Intro = intro,
                    Birthday = item.Birthday,
                    Address = item.Address,
                    Experties = experts,
                    GenderDescription = item.Gender.GenderText
                };
                coachDetailDtos.Add(coachDetailDto);
            }
            return Ok(coachDetailDtos);
        }

        // GET: api/Coach/5
        //取得特定教練資料
        [HttpGet("{id}")]
        public async Task<ActionResult> GetCoach(int id)
        {
            string base64Image = "";
            List<RateDetailDto> rateDetailDtos = new List<RateDetailDto>();
            List<FieldDetailDto> fieldDetailDtos = new List<FieldDetailDto>();
            List<ScheduleDatailDto> scheduleDatailDtos = new List<ScheduleDatailDto>();
            List<ExpertiseDto> expertiseDtos = new List<ExpertiseDto>();

            var coach = await _context.TIdentities.Where(x => x.RoleId == 2 && x.Id == id).Include(x => x.Gender).Include(x => x.Role).Include(x => x.TcoachInfoIds).FirstOrDefaultAsync();
            if (coach == null)
            {
                return NotFound();
            }

            var coachInfo = coach.TcoachInfoIds.FirstOrDefault();
            var experts = await _context.TcoachExperts.Where(x => x.CoachId == id).Include(x => x.Class).ToListAsync();
            var rates = await _context.TmemberRateClasses.Where(x => x.CoachId == id).Include(x => x.Reserve.Member).Include(x => x.Reserve.ClassSchedule.Class).ToListAsync();
            var schedules = await _context.TclassSchedules.Where(x => x.CoachId == id).Include(x => x.CourseTime).Include(x => x.ClassStatus).ToListAsync();
            var fields = await _context.TfieldReserves.Where(x => x.CoachId == id).Include(x => x.Field.Gym.Region.City).ToListAsync();

            if (!string.IsNullOrEmpty(coach.Photo))
            {
                string path = Path.Combine(_env.ContentRootPath, "Images", "CoachImages", coach.Photo);
                byte[] bytes = System.IO.File.ReadAllBytes(path);
                base64Image = Convert.ToBase64String(bytes);
            }
            foreach (var expert in experts)
            {
                ExpertiseDto expertiseDto = new ExpertiseDto()
                {
                    ClassName = expert.Class.ClassName,
                };
                expertiseDtos.Add(expertiseDto);
            }
            CoachDetailDto coachDetailDto = new CoachDetailDto()
            {
                Id = coach.Id,
                Name = coach.Name,
                Phone = coach.Phone,
                EMail = coach.EMail,
                Photo = base64Image,
                Birthday = coach.Birthday,
                Address = coach.Address,
                Intro = coachInfo.CoachIntro,
                Experties = expertiseDtos,
                RoleDescription = coach.Role.RoleDescribe,
                GenderDescription = coach.Gender.GenderText
            };
            foreach (var rate in rates)
            {
                RateDetailDto rateDetailDto = new RateDetailDto()
                {
                    ReserveId = rate.ReserveId,
                    Member = rate.Reserve.Member.Name,
                    Class = rate.Reserve.ClassSchedule.Class.ClassName,
                    RateClass = rate.RateClass,
                    ClassDescribe = rate.ClassDescribe,
                    RateCoach = rate.RateCoach,
                    CoachDescribe = rate.CoachDescribe
                };
                rateDetailDtos.Add(rateDetailDto);
            }
            foreach (var field in fields)
            {
                FieldDetailDto fieldDetailDto = new FieldDetailDto()
                {
                    FieldReserveId = field.FieldReserveId,
                    City = field.Field.Gym.Region.City.City,
                    Region = field.Field.Gym.Region.Region,
                    Gym = field.Field.Gym.Name,
                    Field = field.Field.FieldName,
                    PaymentStatus = field.PaymentStatus,
                    ReserveStatus = field.ReserveStatus
                };
                fieldDetailDtos.Add(fieldDetailDto);
            }
            foreach (var schedule in schedules)
            {
                ScheduleDatailDto scheduleDatailDto = new ScheduleDatailDto()
                {
                    ClassScheduleId = schedule.ClassScheduleId,
                    Class = schedule.Class.ClassName,
                    Coach = schedule.Coach.Name,
                    Field = schedule.Field.FieldName,
                    CourseDate = schedule.CourseDate,
                    CourseTime = schedule.CourseTime.TimeName,
                    MaxStudent = schedule.MaxStudent,
                    ClassStatus = schedule.ClassStatus.ClassStatusDiscribe,
                    ClassPayment = schedule.ClassPayment,
                    CoachPayment = schedule.CoachPayment
                };
                scheduleDatailDtos.Add(scheduleDatailDto);
            }


            var result = new
            {
                coachDetailDto,
                rateDetailDtos,
                scheduleDatailDtos,
                fieldDetailDtos
            };
            return Ok(result);
        }

        // PUT: api/Coach/5
        //修改教練資料
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCoach(PutCoachDto putCoachDto, IFormFile img)
        {
            var coach = await _context.TIdentities.FindAsync(putCoachDto.Id);
            coach.Name = putCoachDto.Name;
            coach.Phone = putCoachDto.Phone;
            coach.Password = putCoachDto.Password;
            coach.EMail = putCoachDto.EMail;
            if (img != null)
            {
                coach.Photo = img.FileName;
            }
            coach.Birthday = putCoachDto.Birthday;
            coach.Address = putCoachDto.Address;
            coach.GenderId = putCoachDto.GenderId;
            await _context.SaveChangesAsync();

            return Ok("教練資訊已成功更新");
        }

        // POST: api/TIdentities
        //新增教練資料
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PutCoachDto>> PostCoach([FromForm] PutCoachDto putCoachDto)
        {
            TIdentity identity = new TIdentity();
            identity.Name = putCoachDto.Name;
            identity.Phone = putCoachDto.Phone;
            identity.Password = putCoachDto.Password;
            identity.EMail = putCoachDto.EMail;
            if (putCoachDto.Photo != null)
            {
                identity.Photo = putCoachDto.Photo.FileName;
                string path = Path.Combine(_env.ContentRootPath, "Images", "CoachImages", putCoachDto.Photo.FileName);
                using (FileStream fileStream = new FileStream(path, FileMode.Create))
                {
                    putCoachDto.Photo.CopyTo(fileStream);
                }
            }
            identity.Birthday = putCoachDto.Birthday;
            identity.Address = putCoachDto.Address;
            identity.GenderId = putCoachDto.GenderId;
            identity.RoleId = putCoachDto.RoleId;
            _context.TIdentities.Add(identity);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCoach", new { id = identity.Id }, putCoachDto);
        }

        // DELETE: api/TIdentities/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTIdentity(int id)
        {
            var tIdentity = await _context.TIdentities.FindAsync(id);
            if (tIdentity == null)
            {
                return NotFound();
            }

            var info = await _context.TcoachInfoIds.Where(x => x.CoachId == id).ToListAsync();
            _context.TcoachInfoIds.RemoveRange(info);
            var expert = await _context.TcoachExperts.Where(x => x.CoachId == id).ToListAsync();
            _context.TcoachExperts.RemoveRange(expert);
            _context.TIdentities.Remove(tIdentity);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TIdentityExists(int id)
        {
            return (_context.TIdentities?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
