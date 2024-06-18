using Microsoft.AspNetCore.Mvc;
using projRESTfulApiFitConnect.DTO;
using projRESTfulApiFitConnect.Models;
using Microsoft.EntityFrameworkCore;
using System.Drawing;
using System.Drawing.Printing;
using System.Drawing.Text;
using projRESTfulApiFitConnect.DTO.Gym;
using System.Linq;
using System.Dynamic;
using Humanizer;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace projRESTfulApiFitConnect.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GymListController : ControllerBase
    {
        private readonly GymContext _context;
        private readonly IWebHostEnvironment _env;

        public GymListController(GymContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // GET: api/GymList
        //讀取所有場館資訊
        [HttpGet(Name = "GetGymLists")]
        public async Task<ActionResult<IEnumerable<GymListDto>>> GetGymList()
        {
            string filepath = "";

            List<GymListDto> gymDtos = new List<GymListDto>();
            if (_context.TGyms == null)
            {
                return NotFound();
            }
            var gyms = await _context.GymInfoDetails
                .Where(x=>x.GymStatus==true)
                .ToListAsync();
            foreach (var item in gyms)
            {
                string base64Image = "";
                filepath = Path.Combine(_env.ContentRootPath, "Images", "GymImages", item.GymPhoto);
                if (System.IO.File.Exists(filepath))
                {
                    byte[] bytes = await System.IO.File.ReadAllBytesAsync(filepath);
                    base64Image = Convert.ToBase64String(bytes);
                }

                GymListDto gymDto = new GymListDto()
                {
                    GymId = item.GymId,
                    RegionId = item.RegionId,
                    Region = item.Region,
                    CityId =item.CityId,
                    City = item.City,
                    GymName=item.GymName,
                    GymAddress=item.GymAddress,
                    GymStatus=item.GymStatus,
                    GymTime=item.GymTime,
                    GymPhoto =base64Image,
                    GymPhone = item.GymPhoto,
                    GymPark=item.GymPark,
                    GymTraffic=item.GymTraffic,
                    GymDescribe=item.GymDescribe
                };
                gymDtos.Add(gymDto);
            }
            return Ok(gymDtos);
        }
        
        // GET api/<GymListController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TGym>> GetTGym(int id)
        {
            List<GymListDto> gymDtos = new List<GymListDto>();
            if (_context.TGyms == null)
            {
                return NotFound();
            }
            var gyms = await _context.TGyms
                .Include(x=>x.Region)
                .ThenInclude(x=>x.City)
                .Where(x => x.GymId == id)
                .Where(x => x.GymStatus == true)
                .ToListAsync();

            foreach (var item in gyms)
            {
                GymListDto gymDto = new GymListDto()
                {
                    GymId = item.GymId,
                    RegionId = item.RegionId,
                    Region = item.Region.Region,
                    CityId = item.Region.City.CityId,
                    City = item.Region.City.City,
                    GymName = item.GymName,
                    GymAddress = item.GymAddress,
                    GymStatus = item.GymStatus,
                    GymTime = item.GymTime,
                    GymPark = item.GymPark,
                    GymTraffic = item.GymTraffic,
                    GymDescribe = item.GymDescribe
                };
                gymDtos.Add(gymDto);
            }
            return Ok(gymDtos);
        }

        //  get all time
        [HttpGet("time")]
        public async Task<IActionResult> GetTimes()
        {
            return Ok(_context.TtimesDetails);
        }

        //POST api/<GymListController>
        [HttpPost]
        public async Task<IActionResult> PostTGym([FromForm] GymDetailDto dto)
        {

            int ownerId, companyId;

            // 檢查負責人是否重複
            Towner owner = _context.Towners.FirstOrDefault(x => x.Owner == dto.Owner);
            if (owner == null)
            {
                // 新增負責人
                owner = new Towner
                {
                    Owner = dto.Owner,
                    Status = false
                };
                _context.Towners.Add(owner);
                await _context.SaveChangesAsync();
                ownerId = owner.OwnerId; // 讀取新負責人的 OwnerId
            }
            else
            {
                ownerId = owner.OwnerId; // 讀取原有負責人的 OwnerId
            }

            // 檢查公司是否重複
            Tcompany company = _context.Tcompanies.FirstOrDefault(x => x.Name == dto.Name);
            if (company == null)
            {
                // 新增公司
                company = new Tcompany
                {
                    OwnerId = ownerId,
                    Name = dto.Name,
                    Timelimit = new DateOnly(2050, 12, 31),
                    Status = false
                };
                _context.Tcompanies.Add(company);
                await _context.SaveChangesAsync();
                companyId = company.CompanyId; // 讀取新公司的 CompanyId
            }
            else
            {
                companyId = company.CompanyId; // 讀取原有公司的 CompanyId
            }

            //讀取表單 場館地區
            string GymRegion = dto.GymRegion;
            int regionId = Convert.ToInt32(_context.TregionTables.FirstOrDefault(x => x.Region == GymRegion).RegionId);
            //讀取表單 開始-結束時間
            string start_time = dto.start_time, end_time = dto.end_time;
            int time1 = Convert.ToInt32(start_time);
            int time2 = Convert.ToInt32(end_time);
            //時間ID轉成字串 "00:00-00:00"
            string text = _context.TtimesDetails.FirstOrDefault(x => x.TimeId == time1).TimeName.ToString(@"hh\:mm")
                + " - " + _context.TtimesDetails.FirstOrDefault(x => x.TimeId == time2).TimeName.ToString(@"hh\:mm");

            // 處理照片上傳
            string gymPhotoFileName = null;
            if (dto.UploadedGymPhoto != null && dto.UploadedGymPhoto.Length > 0)
            {
                var uploads = Path.Combine(Directory.GetCurrentDirectory(), "Images", "GymImages");
                if (!Directory.Exists(uploads))
                {
                    Directory.CreateDirectory(uploads);
                }

                gymPhotoFileName = Path.GetFileName(dto.UploadedGymPhoto.FileName);
                var filePath = Path.Combine(uploads, gymPhotoFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await dto.UploadedGymPhoto.CopyToAsync(fileStream);
                }
            }
            dto.GymPhoto = gymPhotoFileName;
            //新增場館
            TGym newGym = new TGym
            {
                CompanyId = companyId,
                RegionId = regionId,
                GymName = dto.GymName,
                GymAddress = dto.GymAddress,
                GymPhone = dto.GymPhone,
                ExpiryDate = new DateOnly(2050, 12, 31),
                GymTime = text,
                GymPhoto = dto.GymPhoto,
                GymStatus = false,
                GymPark = dto.GymPark,
                GymTraffic = dto.GymTraffic,
                GymDescribe = dto.GymDescribe,
            };
            _context.TGyms.Add(newGym);
            _context.SaveChanges();
            int gymId = newGym.GymId;
            _context.SaveChanges();
            return Ok("gym create success");
        }

        // PUT api/<GymListController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGym(int id, [FromForm] GymDetailDto dto)
        {
            if (id != dto.GymId)
            {
                return BadRequest();
            }

            var gym = await _context.TGyms.FindAsync(id);
            if (gym == null)
            {
                return NotFound();
            }

            int ownerId, companyId;

            // 檢查負責人是否重複
            Towner owner = _context.Towners.FirstOrDefault(x => x.Owner == dto.Owner);
            if (owner == null)
            {
                // 新增負責人
                owner = new Towner
                {
                    Owner = dto.Owner,
                    Status = false
                };
                _context.Towners.Add(owner);
                await _context.SaveChangesAsync();
                ownerId = owner.OwnerId; // 讀取新負責人的 OwnerId
            }
            else
            {
                ownerId = owner.OwnerId; // 讀取原有負責人的 OwnerId
            }

            // 檢查公司是否重複
            Tcompany company = _context.Tcompanies.FirstOrDefault(x => x.Name == dto.Name);
            if (company == null)
            {
                // 新增公司
                company = new Tcompany
                {
                    OwnerId = ownerId,
                    Name = dto.Name,
                    Timelimit = new DateOnly(2050, 12, 31),
                    Status = false
                };
                _context.Tcompanies.Add(company);
                await _context.SaveChangesAsync();
                companyId = company.CompanyId; // 讀取新公司的 CompanyId
            }
            else
            {
                companyId = company.CompanyId; // 讀取原有公司的 CompanyId
            }

            //讀取表單 場館地區
            string GymRegion = dto.GymRegion;
            int regionId = Convert.ToInt32(_context.TregionTables.FirstOrDefault(x => x.Region == GymRegion).RegionId);
            //讀取表單 開始-結束時間
            string start_time = dto.start_time, end_time = dto.end_time;
            int time1 = Convert.ToInt32(start_time);
            int time2 = Convert.ToInt32(end_time);
            //時間ID轉成字串 "00:00-00:00"
            string text = _context.TtimesDetails.FirstOrDefault(x => x.TimeId == time1).TimeName.ToString(@"hh\:mm")
                + " - " + _context.TtimesDetails.FirstOrDefault(x => x.TimeId == time2).TimeName.ToString(@"hh\:mm");

            // 處理照片上傳
            string gymPhotoFileName = gym.GymPhoto;
            if (dto.UploadedGymPhoto != null && dto.UploadedGymPhoto.Length > 0)
            {
                var uploads = Path.Combine(Directory.GetCurrentDirectory(), "Images", "GymImages");
                if (!Directory.Exists(uploads))
                {
                    Directory.CreateDirectory(uploads);
                }

                gymPhotoFileName = Path.GetFileName(dto.UploadedGymPhoto.FileName);
                var filePath = Path.Combine(uploads, gymPhotoFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await dto.UploadedGymPhoto.CopyToAsync(fileStream);
                }

                // 檢查是否有舊的照片,有的話刪除
                var oldPhotoPath = Path.Combine(uploads, gym.GymPhoto);
                if (System.IO.File.Exists(oldPhotoPath))
                {
                    System.IO.File.Delete(oldPhotoPath);
                }
            }

            // 更新場館信息
            gym.CompanyId = companyId;
            gym.RegionId = regionId;
            gym.GymName = dto.GymName;
            gym.GymAddress = dto.GymAddress;
            gym.GymPhone = dto.GymPhone;
            gym.GymTime = text;
            gym.GymPhoto = gymPhotoFileName;
            gym.GymStatus = true;
            gym.GymPark = dto.GymPark;
            gym.GymTraffic = dto.GymTraffic;
            gym.GymDescribe = dto.GymDescribe;

            _context.Entry(gym).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TGymExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }
        private bool TGymExists(int id)
        {
            return (_context.TGyms?.Any(e => e.GymId == id)).GetValueOrDefault();
        }
    }
}
