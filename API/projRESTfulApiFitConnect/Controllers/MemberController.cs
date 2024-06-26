using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using projRESTfulApiFitConnect.Models;
using projRESTfulApiFitConnect.DTO.Member.status;
using projRESTfulApiFitConnect.DTO.Member;
using projRESTfulApiFitConnect.DTO.Product;
using projRESTfulApiFitConnect.DTO.Member.comment;
using projRESTfulApiFitConnect.DTO.Coach;
using Microsoft.IdentityModel.Tokens;

namespace projRESTfulApiFitConnect.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberController : Controller
    {
        private readonly GymContext _context;
        private readonly IWebHostEnvironment _env;
        public MemberController(GymContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MemberDetailDto>>> GetMembers()
        {
            List<MemberDetailDto> memberDetailDtos = await AllMember();
            return Ok(memberDetailDtos);
        }

        [HttpGet("unactivated")]
        public async Task<ActionResult<IEnumerable<MemberDetailDto>>> GetunactivatedMembers()
        {
            List<MemberDetailDto> memberDetailDtos = new List<MemberDetailDto>();

            var members = await _context.TIdentities
                        .Where(x => x.RoleId == 1 && x.Activated == false)
                        .Include(x => x.Gender)
                        .ToListAsync();


            foreach (var item in members)
            {
                MemberDetailDto memberDetailDto = new MemberDetailDto()
                {
                    Id = item.Id,
                    Name = item.Name,
                    Phone = item.Phone,
                    EMail = item.EMail,
                    Birthday = item.Birthday,
                    Address = item.Address,
                    GenderDescription = item.Gender.GenderText,
                    GenderID = item.GenderId
                };
                memberDetailDtos.Add(memberDetailDto);
            }
            return Ok(memberDetailDtos);
        }

        private async Task<List<MemberDetailDto>> AllMember()
        {
            //string filepath = "";

            List<MemberDetailDto> memberDetailDtos = new List<MemberDetailDto>();

            var members = await _context.TIdentities
                        .Where(x => x.RoleId == 1 && x.Activated == true)
                        .Include(x => x.Gender)
                        .ToListAsync();


            foreach (var item in members)
            {

                /*string base64Image = "";
                filepath = Path.Combine(_env.ContentRootPath, "Images", "MemberImages", item.Photo);
                if (System.IO.File.Exists(filepath))
                {
                    byte[] bytes = await System.IO.File.ReadAllBytesAsync(filepath);
                    base64Image = Convert.ToBase64String(bytes);
                }*/

                MemberDetailDto memberDetailDto = new MemberDetailDto()
                {
                    Id = item.Id,
                    Name = item.Name,
                    Phone = item.Phone,
                    EMail = item.EMail,
                    //Photo = base64Image,
                    Birthday = item.Birthday,
                    Address = item.Address,
                    GenderDescription = item.Gender.GenderText,
                    GenderID = item.GenderId
                };
                memberDetailDtos.Add(memberDetailDto);
            }

            return memberDetailDtos;
        }

        [HttpGet("users/{id}")]
        public IActionResult getprofile(int? id)
        {
            var member = _context.TIdentities.Where(x => x.Id == id && x.Activated == true).FirstOrDefault();
            if (member == null)
                return NotFound();

            if (member.Photo != null)
            {
                string path = Path.Combine(_env.ContentRootPath, "Images", "MemberImages", member.Photo);
                if (System.IO.File.Exists(path))
                {
                    byte[] bytes = System.IO.File.ReadAllBytes(path);
                    member.Photo = Convert.ToBase64String(bytes);
                }
                else
                {
                    int random = (new Random()).Next(1, 5);
                    string imgpath = Path.Combine(_env.ContentRootPath, "Images", "MemberImages", "default" + random + ".jpg");
                    if (System.IO.File.Exists(imgpath))
                    {
                        byte[] bytes = System.IO.File.ReadAllBytes(imgpath);
                        member.Photo = Convert.ToBase64String(bytes);
                    }
                }
            }

            return Ok(member);
        }

        [HttpGet("others/{id}")]
        public IActionResult status(int? id)
        {
            ////  get member's other datas by id
            //if (id == null)
            //    return NotFound();

            //if (!_context.TIdentities.Any(x => x.Id == id && x.Activated == true))
            //    return NotFound();

            //MemberProfileDTO mp = new MemberProfileDTO((int)id, _context);
            //var response = new
            //{
            //    reserved = mp.li_reservedDetail,
            //    follow = mp.li_follow,
            //    comments = mp.li_rateClass,
            //};
            //if (mp.status)
            //    return Ok(response);
            return NotFound();
        }

        [HttpPost]
        public IActionResult Post(AddMemberDto dto)
        {
            //  address null !!

            //  add new tidentity
            int r_id = 0;

            //  preprocessing photo
            string photo = "";

            //  preprocessing id for coach
            int p_id = -1;

            switch (r_id)
            {
                case 0:
                    TIdentity member = new TIdentity
                    {
                        RoleId = 1,
                        Name = dto.idName,
                        Phone = dto.idPhone,
                        EMail = dto.idEmail,
                        Password = dto.idPwd,
                        Photo = photo,
                        Birthday = dto.idBirthday,
                        GenderId = dto.idGender,
                        Activated = true,
                        Payment = 0,
                    };
                    _context.TIdentities.Add(member);
                    _context.SaveChanges();
                    return Ok(member);
                case 1:
                    bool isProcess = _context.TIdentities.Any(x => x.Id == p_id && x.RoleId == 1 && x.Activated == true);
                    if (!isProcess)
                        return NotFound();
                    TIdentity coach = _context.TIdentities.FirstOrDefault(x => x.Id == p_id && x.RoleId == 1 && x.Activated == true);
                    if (coach.Birthday.ToDateTime(TimeOnly.MinValue) < DateTime.Now.AddYears(-18))
                        return Ok("not old enouth");
                    if (coach.Payment != 0)
                        return Ok("isfine");
                    coach.RoleId = 2;
                    _context.SaveChanges();
                    break;
                default:
                    return NotFound();
            }
            return NotFound();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetMember(int id)
        {
            var member = await GetMemberByIdAsync(id);

            if (member == null)
            {
                return NotFound();
            }

            var base64Image = await GetBase64ImageAsync(member.Photo);

            List<TclassReserve> courses = await GetDistinctClassReservesAsync(id);

            var comments = await GetCommentsAsync(id);

            var followAndBlackList = await GetFollowAndBlackList(id);

            var result = new
            {
                memberDetailDto = MapToMemberDetailDto(member, base64Image),
                //courses,
                reserveDetailDtos = await MapToReserveDetailDtosAsync(courses),
                comments,
                followAndBlackList
            };

            return Ok(result);
        }

        private async Task<FollowAndBlackListDto> GetFollowAndBlackList(int id)
        {
            var followAndBlackList = await _context.TmemberFollows.Where(x => x.MemberId == id).Include(x => x.Status).Include(x => x.Coach).ToListAsync();
            List<C_memberfollow> follow = followAndBlackList.Where(x => x.StatusId == 1).Select(x => new C_memberfollow { id = x.CoachId, name = x.Coach.Name, st_id = 1, status = x.Status.StatusDescribe, experts = _context.TcoachExperts.Where(a => a.CoachId == x.CoachId).Select(a => a.Class.ClassName).ToList() }).ToList();
            List<C_memberfollow> blackList = followAndBlackList.Where(x => x.StatusId == 2).Select(x => new C_memberfollow { id = x.CoachId, name = x.Coach.Name, st_id = 2, status = x.Status.StatusDescribe, experts = _context.TcoachExperts.Where(a => a.CoachId == x.CoachId).Select(a => a.Class.ClassName).ToList() }).ToList();
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

        private Task<TIdentity> GetMemberByIdAsync(int id)
        {
            return _context.TIdentities
                .Where(x => x.Id == id)
                .Include(x => x.Gender)
                .Include(x => x.Role)
                .FirstOrDefaultAsync();

            //  to make coach available
            //  .Where(x => x.RoleId == 1 && x.Id == id)
        }

        private async Task<string> GetBase64ImageAsync(string photo)
        {
            string imagePath = Path.Combine(_env.ContentRootPath, @"Images\MemberImages", photo);

            if (string.IsNullOrEmpty(photo) || !System.IO.File.Exists(imagePath))
                imagePath = Path.Combine(_env.ContentRootPath, @"Images\MemberImages", "default1.jpg");

            var bytes = await System.IO.File.ReadAllBytesAsync(imagePath);

            return Convert.ToBase64String(bytes);
        }

        private async Task<List<TclassReserve>> GetDistinctClassReservesAsync(int memberId)
        {
            return await _context.TclassReserves
                .Where(x => x.MemberId == memberId)
                .Include(x => x.ClassSchedule.Class)
                .Include(x => x.ClassSchedule.CourseStartTime)
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
            List<ReserveDetailDto> reserveDetailDtos = new List<ReserveDetailDto>();

            foreach (var item in courses)
            {
                var sameCourseButTime = await _context.TclassSchedules
                    .Where(x => x.ClassId == item.ClassSchedule.ClassId
                                && x.CoachId == item.ClassSchedule.CoachId
                                && x.FieldId == item.ClassSchedule.FieldId
                                && x.CourseDate == item.ClassSchedule.CourseDate)
                    .Include(x => x.CourseStartTime)
                    .ToListAsync();

                var timeSpans = sameCourseButTime.Select(time => time.CourseStartTime.TimeName).ToList();

                var dateAndTimeDto = new DateAndTimeDto
                {
                    date = sameCourseButTime.FirstOrDefault().CourseDate,
                    timeList = [timeSpans.Min().ToTimeSpan(), timeSpans.Max().ToTimeSpan()],
                };

                var reserveDetailDto = new ReserveDetailDto
                {
                    ReserveId = item.ReserveId,
                    Class = item.ClassSchedule.Class.ClassName,
                    Schedule_id = item.ClassSchedule.ClassScheduleId,
                    Coach = item.ClassSchedule.Coach.Name,
                    Address = item.ClassSchedule.Field.Gym.GymAddress,
                    Gym = item.ClassSchedule.Field.Gym.GymName,
                    Field = item.ClassSchedule.Field.FieldName,
                    Time = dateAndTimeDto,
                    MaxStudent = item.ClassSchedule.MaxStudent,
                    CourseFee = item.ClassSchedule.ClassPayment,
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
         public async Task<IActionResult> PutMember(int id ,PutMemberDto putMemberDto)
        {
            var member = await _context.TIdentities.FindAsync(id);

            if (member == null)
            {
                return NotFound("not found");
            }
            if (!string.IsNullOrEmpty(putMemberDto.Name))
                member.Name = putMemberDto.Name;
            if (!string.IsNullOrEmpty(putMemberDto.Phone))
                member.Phone = putMemberDto.Phone;
            if (!string.IsNullOrEmpty(putMemberDto.EMail))
                member.EMail = putMemberDto.EMail;
            if (!string.IsNullOrEmpty(putMemberDto.Address))
                member.Address = putMemberDto.Address;
            if(!string.IsNullOrEmpty(putMemberDto.Photo) && !string.IsNullOrEmpty(putMemberDto.ImageBase64))
            {
                byte[] imageBytes = Convert.FromBase64String(putMemberDto.ImageBase64);
                string filepath = Path.Combine(_env.ContentRootPath, "Images", "MemberImages", putMemberDto.Photo);
                await System.IO.File.WriteAllBytesAsync(filepath, imageBytes);

                member.Photo = putMemberDto.Photo;
            }

            _context.Entry(member).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok("會員資訊已成功更新");
        }

        [HttpPost]
        [Route("SEARCH")]
        public async Task<ActionResult<MemberPagingDTO>> GetCoachesSearch(/*[FromQuery]*/ MemberSearchDTO memberSearchDTO)
        {
            List<MemberDetailDto> memberDetailDtos = await AllMember();
            //根據分類搜尋教練資料
            var everyMember = memberSearchDTO.gender == 0 ? memberDetailDtos : memberDetailDtos.Where(s => s.GenderID == memberSearchDTO.gender);
            //根據關鍵字搜尋會員資料(姓名、性別、教練資訊、專長名稱)
            if (!string.IsNullOrEmpty(memberSearchDTO.keyword))
            {
                everyMember = everyMember.Where(s => s.Name.Contains(memberSearchDTO.keyword) ||
                 s.GenderDescription.Contains(memberSearchDTO.keyword));
            }

            //排序
            switch (memberSearchDTO.sortBy)
            {
                //依年齡
                case "Ages":
                    everyMember = memberSearchDTO.sortType == "asc" ? everyMember.OrderBy(s => s.Birthday) : everyMember.OrderByDescending(s => s.Birthday);
                    break;
                //依性別
                case "GenderID":
                    everyMember = memberSearchDTO.sortType == "asc" ? everyMember.OrderBy(s => s.GenderID) : everyMember.OrderByDescending(s => s.GenderID);
                    break;
                //預設為id
                default:
                    everyMember = memberSearchDTO.sortType == "asc" ? everyMember.OrderBy(s => s.Id) : everyMember.OrderByDescending(s => s.Id);
                    break;
            }
            //int totalrate = everyCoach.
            //總共有多少筆資料
            int totalCount = everyMember.Count();
            //每頁要顯示幾筆資料
            int pageSize = (int)memberSearchDTO.pageSize;
            //目前第幾頁
            int page = (int)memberSearchDTO.page;
            //計算總共有幾頁
            int totalPages = (int)Math.Ceiling((decimal)totalCount / pageSize);
            //分頁
            everyMember = everyMember.Skip((page - 1) * pageSize).Take(pageSize);
            //包裝要傳給client端的資料
            MemberPagingDTO memberPaging = new MemberPagingDTO();
            memberPaging.TotalCount = totalCount;
            memberPaging.TotalPages = totalPages;
            memberPaging.MemberResult = everyMember.ToList();
            return Ok(memberPaging);
        }

        [HttpPut("{id}/become-coach")]
        public async Task<IActionResult> BecomeCoach(int id, BecomeCoachDTO becomeCoachDTO)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                var newcoach = await _context.TIdentities
                    .Where(x => x.Id == id && x.RoleId == 1)
                    .FirstOrDefaultAsync();

                if (newcoach == null)
                {
                    return NotFound("User not found or not eligible to become a coach");
                }

                newcoach.RoleId = 4;

                if (!string.IsNullOrEmpty(becomeCoachDTO.coachName))
                    newcoach.Name = becomeCoachDTO.coachName;

                if (becomeCoachDTO.expert != null)
                {
                    foreach (var expertId in becomeCoachDTO.expert)
                    {
                        TcoachExpert expert = new TcoachExpert
                        {
                            CoachId = newcoach.Id,
                            ClassId = expertId
                        };
                        _context.TcoachExperts.Add(expert);
                    }
                }

                if (!string.IsNullOrEmpty(becomeCoachDTO.intro))
                {
                    bool isProcess = _context.TcoachInfoIds.Any(x => x.CoachId == newcoach.Id);
                    if (isProcess) { return Ok("RegisterisProcessing."); }
                    TcoachInfoId intro = new TcoachInfoId
                    {
                        CoachId = newcoach.Id,
                        CoachIntro = becomeCoachDTO.intro
                    };
                    _context.TcoachInfoIds.Add(intro);
                }

                if (!string.IsNullOrEmpty(becomeCoachDTO.photo) && !string.IsNullOrEmpty(becomeCoachDTO.ImageBase64))
                {
                    byte[] imageBytes = Convert.FromBase64String(becomeCoachDTO.ImageBase64);
                    string filepath = Path.Combine(_env.ContentRootPath, "Images", "CoachImages", becomeCoachDTO.photo);

                    Directory.CreateDirectory(Path.GetDirectoryName(filepath));
                    await System.IO.File.WriteAllBytesAsync(filepath, imageBytes);
                    newcoach.Photo = becomeCoachDTO.photo;
                }
                _context.Entry(newcoach).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                // Save additional images if any
                if (becomeCoachDTO.Images != null && becomeCoachDTO.moreBase64Images != null && becomeCoachDTO.Images.Count == becomeCoachDTO.moreBase64Images.Count)
                {
                    for (int i = 0; i < becomeCoachDTO.Images.Count; i++)
                    {
                        byte[] additionalImageBytes = Convert.FromBase64String(becomeCoachDTO.moreBase64Images[i]);

                        string contentRootPath = _env.ContentRootPath;
                        string imageFolder = "Images";
                        string coachImageFolder = "CoachImages";
                        string coachImageName = becomeCoachDTO.Images[i].coachImages;

                        string additionalFilePath = Path.Combine(contentRootPath, imageFolder, coachImageFolder, coachImageName);
                        Directory.CreateDirectory(Path.GetDirectoryName(additionalFilePath));

                        await System.IO.File.WriteAllBytesAsync(additionalFilePath, additionalImageBytes);

                        TcoachPhoto coachPhoto = new TcoachPhoto
                        {
                            Id = newcoach.Id,
                            CoachPhoto = coachImageName
                        };
                        _context.TcoachPhotos.Add(coachPhoto);
                    }
                    await _context.SaveChangesAsync();
                }
                await transaction.CommitAsync();
                return Ok("requesting...");


            }
        }
        // DELETE:
        [HttpDelete("{id}/suspend")]
        public async Task<IActionResult> inactiveteMember(int id)
        {
            var tIdentity = await _context.TIdentities.FindAsync(id);
            if (tIdentity == null)
            {
                return NotFound();
            }

            tIdentity.Activated = false;
            await _context.SaveChangesAsync();

            return Ok("suspend");
        }
        // DELETE:
        [HttpDelete("{id}/activate")]
        public async Task<IActionResult> activeteMember(int id)
        {
            var tIdentity = await _context.TIdentities.FindAsync(id);
            if (tIdentity == null)
            {
                return NotFound();
            }

            tIdentity.Activated = true;
            await _context.SaveChangesAsync();

            return Ok("Activated");
        }
    }
}
