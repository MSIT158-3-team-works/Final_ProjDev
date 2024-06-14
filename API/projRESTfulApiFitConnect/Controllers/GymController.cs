using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using projRESTfulApiFitConnect.DTO;
using projRESTfulApiFitConnect.DTO.Gym;
using projRESTfulApiFitConnect.Models;


namespace projRESTfulApiFitConnect.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GymController : ControllerBase
    {
        private readonly GymContext _context;
        private readonly IWebHostEnvironment _env;

        public GymController(GymContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        // GET api/Gym
        [HttpGet(Name = "GetGyms")]
        public async Task<ActionResult<IEnumerable<GymDto>>> GetGyms()
        {
            string filepath = "";

            List<GymDto> fieldinfoDtos = new List<GymDto>();
            if (_context.TfieldPhotos == null)
            {
                return NotFound();
            }
            var fields = await _context.TGyms
                                .Include(x =>x.Tfields)
                                .ToListAsync();
            foreach (var item in fields)
            {

                GymDto fieldDto = new GymDto()
                {
                    GymName = item.GymName,
                    GymAddress = item.GymAddress,
                    GymTime = item.GymTime,
                    GymPark = item.GymPark,
                    GymTraffic = item.GymTraffic,
                    GymDescribe = item.GymDescribe
                };
                fieldinfoDtos.Add(fieldDto);
            }
            return Ok(fieldinfoDtos);
        }

        // POST api/<GymController>
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
            //新增場地
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
            //新增場地時間
            for (int i = time1; i <= time2; i++)
            {
                TGymTime gymTime = new TGymTime
                {
                    GymId = gymId,
                    GymTime = i
                };
                _context.TGymTimes.Add(gymTime);
            }
            _context.SaveChanges();
            return Ok("123");
        }

        // PUT api/<GymController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTfield(int id, Tfield field)
        {
            if (id != field.FieldId)
            {
                return BadRequest();
            }

            _context.Entry(field).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TfieldExists(id))
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
        private bool TfieldExists(int id)
        {
            return (_context.Tfields?.Any(e => e.FieldId == id)).GetValueOrDefault();
        }
    }
}
