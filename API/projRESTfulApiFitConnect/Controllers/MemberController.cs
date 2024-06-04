using System;
using System.Collections.Generic;
using System.Composition;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using projRESTfulApiFitConnect.DTO.Coach;
using projRESTfulApiFitConnect.DTO.Member;
using projRESTfulApiFitConnect.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace projRESTfulApiFitConnect.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberController : ControllerBase
    {
        private readonly GymContext _context;
        private readonly IWebHostEnvironment _env;

        public MemberController(GymContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // GET: api/Member
        // 取得所有會員資料(個人資料)
        [HttpGet]
        public async Task<ActionResult> GetMembers()
        {
            string filepath = "";

            List<MemberDetailDto> memberDetailDtos = new List<MemberDetailDto>();

            var members = await _context.TIdentities
                        .Where(x => x.RoleId == 1)
                        .Include(x => x.Gender)
                        .Include(x => x.Role)
                        .ToListAsync();

            foreach (var item in members)
            {
                string base64Image = "";
                filepath = Path.Combine(_env.ContentRootPath, "Images", "MemberImages", item.Photo);
                if (System.IO.File.Exists(filepath))
                {
                    byte[] bytes = await System.IO.File.ReadAllBytesAsync(filepath);
                    base64Image = Convert.ToBase64String(bytes);
                }

                MemberDetailDto memberDetailDto = new MemberDetailDto()
                {
                    Id = item.Id,
                    Name = item.Name,
                    Phone = item.Phone,
                    EMail = item.EMail,
                    Photo = base64Image,
                    Birthday = item.Birthday,
                    Address = item.Address,
                    GenderDescription = item.Gender.GenderText,
                    RoleDescription = item.Role.RoleDescribe
                };
                memberDetailDtos.Add(memberDetailDto);
            }
            return Ok(memberDetailDtos);
        }

        // GET: api/Member/5
        // 取得特定會員資料(個人資料、預約課程、評論紀錄、追蹤)
        [HttpGet("{id}")]
        public async Task<ActionResult> GetMember(int id)
        {
            var member = await GetMemberByIdAsync(id);

            if (member == null)
            {
                return NotFound();
            }

            var base64Image = await GetBase64ImageAsync(member.Photo);

            var courses = await GetDistinctClassReservesAsync(id);

            var comments = await GetCommentsAsync(id);

            var followAndBlackList = await GetFollowAndBlackList(id);

            var result = new
            {
                memberDetailDto = MapToMemberDetailDto(member, base64Image),
                reserveDetailDtos = await MapToReserveDetailDtosAsync(courses),
                comments,
                followAndBlackList
            };

            return Ok(result);
        }

        private async Task<FollowAndBlackListDto> GetFollowAndBlackList(int id)
        {
            var followAndBlackList = await _context.TmemberFollows.Where(x => x.MemberId == id).Include(x => x.Status).Include(x => x.Coach).ToListAsync();
            var follow = followAndBlackList.Where(x => x.StatusId == 1).Select(x => x.Coach.Name).ToList();
            var blackList = followAndBlackList.Where(x => x.StatusId == 2).Select(x => x.Coach.Name).ToList();
            FollowAndBlackListDto followAndBlackListDto = new FollowAndBlackListDto()
            {
                Follow = follow,
                BlackList = blackList,
            };
            return followAndBlackListDto;
        }

        private async Task<List<RateDetailDto>> GetCommentsAsync(int id)
        {
            var rates = await _context.TmemberRateClasses.Where(x => x.MemberId == id).Include(x => x.Reserve.Member).Include(x => x.Coach).ToListAsync();
            List<RateDetailDto> comments = new List<RateDetailDto>();
            foreach (var rate in rates)
            {
                RateDetailDto rateDetailDto = new RateDetailDto()
                {
                    ReserveId = rate.ReserveId,
                    Member = rate.Reserve.Member.Name,
                    Coach = rate.Coach.Name,
                    RateCoach = rate.RateCoach,
                    CoachDescribe = rate.CoachDescribe,
                    Class = rate.Reserve.ClassSchedule.Class.ClassName,
                    RateClass = rate.RateClass,
                    ClassDescribe = rate.ClassDescribe
                };
                comments.Add(rateDetailDto);
            }
            return comments;
        }

        private async Task<TIdentity> GetMemberByIdAsync(int id)
        {
            return await _context.TIdentities
                .Where(x => x.RoleId == 1 && x.Id == id)
                .Include(x => x.Gender)
                .Include(x => x.Role)
                .FirstOrDefaultAsync();
        }

        private async Task<string> GetBase64ImageAsync(string photo)
        {
            if (string.IsNullOrEmpty(photo))
            {
                return string.Empty;
            }

            var imagePath = Path.Combine(_env.ContentRootPath, @"Images\MemberImages", photo);

            if (!System.IO.File.Exists(imagePath))
            {
                return string.Empty;
            }

            var bytes = await System.IO.File.ReadAllBytesAsync(imagePath);

            return Convert.ToBase64String(bytes);
        }

        private async Task<List<TclassReserve>> GetDistinctClassReservesAsync(int memberId)
        {
            return await _context.TclassReserves
                .Where(x => x.MemberId == memberId)
                .Include(x => x.ClassSchedule.Class)
                .Include(x => x.ClassSchedule.CourseTime)
                .Include(x => x.ClassSchedule.Coach)
                .Include(x => x.ClassSchedule.Field.Gym.Region.City)
                .GroupBy(x => new { x.ClassSchedule.ClassId, x.ClassSchedule.CoachId, x.ClassSchedule.FieldId, x.ClassSchedule.CourseDate })
                .Select(group => group.First())
                .ToListAsync();
        }

        private MemberDetailDto MapToMemberDetailDto(TIdentity member, string base64Image)
        {
            return new MemberDetailDto
            {
                Id = member.Id,
                Name = member.Name,
                Phone = member.Phone,
                EMail = member.EMail,
                Photo = base64Image,
                Birthday = member.Birthday,
                Address = member.Address,
                GenderDescription = member.Gender.GenderText,
                RoleDescription = member.Role.RoleDescribe
            };
        }

        private async Task<List<ReserveDetailDto>> MapToReserveDetailDtosAsync(List<TclassReserve> courses)
        {
            var reserveDetailDtos = new List<ReserveDetailDto>();

            foreach (var item in courses)
            {
                var sameCourseButTime = await _context.TclassSchedules
                    .Where(x => x.ClassId == item.ClassSchedule.ClassId
                                && x.CoachId == item.ClassSchedule.CoachId
                                && x.FieldId == item.ClassSchedule.FieldId
                                && x.CourseDate == item.ClassSchedule.CourseDate)
                    .Include(x => x.CourseTime)
                    .ToListAsync();

                var timeSpans = sameCourseButTime.Select(time => time.CourseTime.TimeName).ToList();

                var dateAndTimeDto = new DateAndTimeDto
                {
                    date = sameCourseButTime.FirstOrDefault()?.CourseDate,
                    timeList = timeSpans
                };

                var reserveDetailDto = new ReserveDetailDto
                {
                    ReserveId = item.ReserveId,
                    Class = item.ClassSchedule.Class.ClassName,
                    Coach = item.ClassSchedule.Coach.Name,
                    City = item.ClassSchedule.Field.Gym.Region.City.City,
                    Region = item.ClassSchedule.Field.Gym.Region.Region,
                    Gym = item.ClassSchedule.Field.Gym.Name,
                    Field = item.ClassSchedule.Field.FieldName,
                    Time = dateAndTimeDto,
                    MaxStudent = item.ClassSchedule.MaxStudent,
                    PaymentStatus = item.PaymentStatus,
                    ReserveStatus = item.ReserveStatus
                };

                reserveDetailDtos.Add(reserveDetailDto);
            }

            return reserveDetailDtos;
        }


        // PUT: api/Member/5
        // 修改會員資料
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCoach(PutMemberDto putMemberDto)
        {
            var member = await _context.TIdentities.FindAsync(putMemberDto.Id);
            member.Name = putMemberDto.Name;
            member.Phone = putMemberDto.Phone;
            member.Password = putMemberDto.Password;
            member.EMail = putMemberDto.EMail;
            if (putMemberDto.Photo != null)
            {
                string fileName = Guid.NewGuid() + putMemberDto.Photo.FileName;
                string path = Path.Combine(_env.ContentRootPath, @"Images\MemberImages", fileName);
                using (FileStream fs = new FileStream(path, FileMode.Create))
                {
                    putMemberDto.Photo.CopyTo(fs);
                }
                member.Photo = fileName;
            }
            member.Birthday = putMemberDto.Birthday;
            member.Address = putMemberDto.Address;
            member.GenderId = putMemberDto.GenderId;
            await _context.SaveChangesAsync();

            return Ok("會員資訊已成功更新");
        }

        // POST: api/Member
        // 新增會員資料
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PutMemberDto>> PostMember([FromForm] PutMemberDto putMemberDto)
        {
            TIdentity member = new TIdentity();
            member.Name = putMemberDto.Name;
            member.Phone = putMemberDto.Phone;
            member.Password = putMemberDto.Password;
            member.EMail = putMemberDto.EMail;
            if (putMemberDto.Photo != null)
            {
                member.Photo = putMemberDto.Photo.FileName;
                string path = Path.Combine(_env.ContentRootPath, "Images", "MemberImages", putMemberDto.Photo.FileName);
                using (FileStream fileStream = new FileStream(path, FileMode.Create))
                {
                    putMemberDto.Photo.CopyTo(fileStream);
                }
            }
            member.Birthday = putMemberDto.Birthday;
            member.Address = putMemberDto.Address;
            member.GenderId = putMemberDto.GenderId;
            member.RoleId = putMemberDto.RoleId;
            _context.TIdentities.Add(member);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMember", new { id = member.Id }, putMemberDto);
        }

        // DELETE: api/Member/5
        // 刪除會員所有資料(真的全部刪掉)
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMember(int id)
        {
            var member = await _context.TIdentities.FindAsync(id);
            if (member == null)
            {
                return NotFound();
            }

            var rates = await _context.TmemberRateClasses.Where(x => x.MemberId == id).ToListAsync();
            _context.TmemberRateClasses.RemoveRange(rates);
            var reservedCourses = await _context.TclassReserves.Where(x => x.MemberId == id).ToListAsync();
            _context.TclassReserves.RemoveRange(reservedCourses);
            var followAndBlackLisst = await _context.TmemberFollows.Where(x => x.MemberId == id).ToListAsync();
            _context.TmemberFollows.RemoveRange(followAndBlackLisst);

            _context.TIdentities.Remove(member);
            await _context.SaveChangesAsync();

            return Ok("會員資料刪除成功");
        }

        private bool TIdentityExists(int id)
        {
            return (_context.TIdentities?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
