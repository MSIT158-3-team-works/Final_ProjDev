using System;
using System.Collections.Generic;
using System.Composition;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using projRESTfulApiFitConnect.DTO.Coach;
using projRESTfulApiFitConnect.DTO.Gym;
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
            List<CoachDetailDto> coachDetailDtos = await coachesDetail();
            return Ok(coachDetailDtos);
        }

        private async Task<List<CoachDetailDto>> coachesDetail()
        {
            string filepath = "";

            List<CoachDetailDto> coachDetailDtos = new List<CoachDetailDto>();

            var coaches = await _context.TIdentities
                        .Where(x => x.RoleId == 2)
                        .Include(x => x.Gender)
                        .Include(x => x.TcoachInfoIds)
                        .Include(x => x.TcoachExperts)
                        .ThenInclude(te => te.Class)
                        .Include(x => x.TcoachExperts)
                        .ThenInclude(te => te.Class.ClassSort1)//有氧、無氧、其他
                        .Include(x => x.TcoachExperts)
                        .ThenInclude(te => te.Class.ClassSort2)//課程種類
                        .Include(x => x.TfieldReserves)
                        .ThenInclude(fr => fr.Field.Gym.Region)
                        .Include(x => x.TfieldReserves)
                        .ThenInclude(fr => fr.Field.Gym.Region.City)
                        .ToListAsync();

            foreach (var item in coaches)
            {
                var experts = item.TcoachExperts.Select(te => new ExpertiseDto
                {
                    ClassName = te.Class.ClassName,
                    ClassSort1 = te.Class.ClassSort1.ClassSort1Detail.ToString(),
                    ClassSort2 = te.Class.ClassSort2.ClassSort2Detail.ToString()
                }).ToList();
                var regions = item.TfieldReserves.Select(fr => new CityDto
                {
                    City = fr.Field.Gym.Region.City.City.ToString(),
                    Region = fr.Field.Gym.Region.Region.ToString()
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
                    GenderDescription = item.Gender.GenderText,
                    Region = regions
                };
                coachDetailDtos.Add(coachDetailDto);
            }

            return coachDetailDtos;
        }

        [HttpPost]
        [Route("SEARCH")]
        public async Task<ActionResult<CoachPagingDTO>> GetCoachesSearch(/*[FromQuery]*/ CoachSearchDTO coachSearchDTO)
        {
            List<CoachDetailDto> coachDetailDtos = await coachesDetail();
            //根據分類搜尋教練資料
            var everyCoach = coachSearchDTO.gender == null || coachSearchDTO.gender == "" ? coachDetailDtos : coachDetailDtos.Where(s => s.GenderDescription == coachSearchDTO.gender);
            everyCoach = coachSearchDTO.sort1 == null || coachSearchDTO.sort1 == "" ? everyCoach : everyCoach.Where(s => s.Experties[0].ClassSort1 == coachSearchDTO.sort1);
            everyCoach = coachSearchDTO.sort2 == null || coachSearchDTO.sort2 == "" ? everyCoach : everyCoach.Where(s => s.Experties[0].ClassSort2 == coachSearchDTO.sort2);
            everyCoach = coachSearchDTO.city == null || coachSearchDTO.city == "" ? everyCoach : everyCoach.Where(s => s.Region[0].City == coachSearchDTO.city).Distinct();
            everyCoach = coachSearchDTO.region == null || coachSearchDTO.region == "" ? everyCoach : everyCoach.Where(s => s.Region[0].Region == coachSearchDTO.region);
            //根據關鍵字搜尋教練資料(姓名、性別、教練資訊、專長名稱)
            if (!string.IsNullOrEmpty(coachSearchDTO.keyword))
            {
                everyCoach = everyCoach.Where(s => s.Name.Contains(coachSearchDTO.keyword) ||
                 s.GenderDescription.Contains(coachSearchDTO.keyword) ||
                 s.Experties.Any(e => e.ClassName.Contains(coachSearchDTO.keyword)) ||
                 s.Experties.Any(e => e.ClassSort1.Contains(coachSearchDTO.keyword)) ||
                 s.Experties.Any(e => e.ClassSort2.Contains(coachSearchDTO.keyword)) ||
                 s.Intro.Contains(coachSearchDTO.keyword));
            }

            //排序
            switch (coachSearchDTO.sortBy)
            {
                //依性別
                case "GenderDescription":
                    everyCoach = coachSearchDTO.sortType == "asc" ? everyCoach.OrderBy(s => s.GenderDescription) : everyCoach.OrderByDescending(s => s.GenderDescription);
                    break;
                //預設為id
                default:
                    everyCoach = coachSearchDTO.sortType == "asc" ? everyCoach.OrderBy(s => s.Id) : everyCoach.OrderByDescending(s => s.Id);
                    break;
            }
            //總共有多少筆資料
            int totalCount = everyCoach.Count();
            //每頁要顯示幾筆資料
            int pageSize = (int)coachSearchDTO.pageSize;
            //目前第幾頁
            int page = (int)coachSearchDTO.page;
            //計算總共有幾頁
            int totalPages = (int)Math.Ceiling((decimal)totalCount / pageSize);
            //分頁
            everyCoach = everyCoach.Skip((page - 1) * pageSize).Take(pageSize);
            //包裝要傳給client端的資料
            CoachPagingDTO coachPaging = new CoachPagingDTO();
            coachPaging.TotalCount = totalCount;
            coachPaging.TotalPages = totalPages;
            coachPaging.CoachResult = everyCoach.ToList();
            return Ok(coachPaging);
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
            var schedules = await _context.TclassSchedules.Where(x => x.CoachId == id).Include(x => x.CourseTimeId).Include(x => x.ClassStatus).ToListAsync();
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
                    Gym = field.Field.Gym.GymName,
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
                    CourseTime = schedule.CourseTimeId,
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
            //coach.Birthday = putCoachDto.Birthday;
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
            //identity.Birthday = putCoachDto.Birthday;
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
