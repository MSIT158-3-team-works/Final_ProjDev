using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using projRESTfulApiFitConnect.DTO;
using projRESTfulApiFitConnect.DTO.Gym;
using projRESTfulApiFitConnect.Models;
using System;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace projRESTfulApiFitConnect.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminGymlistController : ControllerBase
    {
        private readonly GymContext _context;
        private readonly IWebHostEnvironment _env;

        public AdminGymlistController(GymContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        //GET api/<AdminGymListController>
        [HttpGet(Name = "GetGymList")]
        public async Task<ActionResult<IEnumerable<GymReviewDetailDto>>> GetGymsList()
        {
            string filepath = "";

            List<GymReviewDetailDto> gymDtos = new List<GymReviewDetailDto>();
            if (_context.TGyms == null)
            {
                return NotFound();
            }
            var gyms = await _context.TGyms
         .Include(x => x.Company)
         .ThenInclude(c => c.Owner)
         .Include(x => x.Region)
         .ThenInclude(r => r.City)
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

                var ownerName = item.Company?.Owner?.Owner ?? "Unknown Owner";
                var companyName = item.Company?.Name ?? "Unknown Company";
                var regionName = item.Region?.Region ?? "Unknown Region";
                var cityName = item.Region?.City?.City ?? "Unknown City";

                string startTime = "", endTime = "";
                if (!string.IsNullOrEmpty(item.GymTime))
                {
                    var times = item.GymTime.Split('-');
                    if (times.Length == 2)
                    {
                        startTime = times[0];
                        endTime = times[1];
                    }
                }

                GymReviewDetailDto gymDto = new GymReviewDetailDto()
                {
                    GymId = item.GymId,
                    Owner = ownerName,
                    RegionId = item.RegionId,
                    Name = companyName,
                    CompanyId = item.CompanyId,
                    GymName = item.GymName,
                    GymAddress = item.GymAddress,
                    GymPhoto = base64Image,
                    GymPhone = item.GymPhone,
                    GymPark = item.GymPark,
                    GymTraffic = item.GymTraffic,
                    GymDescribe = item.GymDescribe,
                    GymStatus = item.GymStatus,
                    Region = regionName,
                    City = cityName,
                    CityId = item.Region.CityId,
                    start_time = startTime,
                    end_time = endTime,
                };
                gymDtos.Add(gymDto);
            }
            return Ok(gymDtos);
        }
        // GET api/<AdminGymListController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GymReviewDetailDto>> GetGymReviewDetail(int id)
        {
            var item = await _context.TGyms
             .Include(x => x.Company)
                 .ThenInclude(c => c.Owner)
             .Include(x => x.Region)
                 .ThenInclude(r => r.City)
             .FirstOrDefaultAsync(x => x.GymId == id);

            if (item == null)
            {
                return NotFound();
            }

            string base64Image = "";
            string filepath = Path.Combine(_env.ContentRootPath, "Images", "GymImages", item.GymPhoto);
            if (System.IO.File.Exists(filepath))
            {
                byte[] bytes = await System.IO.File.ReadAllBytesAsync(filepath);
                base64Image = Convert.ToBase64String(bytes);
            }

            var ownerName = item.Company?.Owner?.Owner ?? "Unknown Owner";
            var companyName = item.Company?.Name ?? "Unknown Company";
            var regionName = item.Region?.Region ?? "Unknown Region";
            var cityName = item.Region?.City?.City ?? "Unknown City";

            string startTime = "", endTime = "";
            if (!string.IsNullOrEmpty(item.GymTime))
            {
                var times = item.GymTime.Split('-');
                if (times.Length == 2)
                {
                    startTime = times[0];
                    endTime = times[1];
                }
            }

            GymReviewDetailDto gymDto = new GymReviewDetailDto()
            {
                GymId = item.GymId,
                Owner = ownerName,
                RegionId = item.RegionId,
                Name = companyName,
                CompanyId = item.CompanyId,
                GymName = item.GymName,
                GymAddress = item.GymAddress,
                GymTime = item.GymTime,
                GymPhoto = base64Image,
                GymPhone = item.GymPhone,
                GymPark = item.GymPark,
                GymTraffic = item.GymTraffic,
                GymDescribe = item.GymDescribe,
                GymStatus = item.GymStatus,
                Region = regionName,
                City = cityName,
                CityId = item.Region.CityId,
                start_time = startTime,
                end_time = endTime,
            };

            return Ok(gymDto);
        }
        
        //PUT api/<AdminGymListController>
        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateGymStatus(int id, [FromBody] GymStatusUpdateDto dto)
        {
            if (dto == null)
            {
                return BadRequest("Invalid data.");
            }

            //讀取表單並轉換 開始-結束時間
            if (string.IsNullOrEmpty(dto.start_time) || string.IsNullOrEmpty(dto.end_time))
            {
                return BadRequest("Invalid data: Start time or end time is null or empty.");
            }

            if (!TimeOnly.TryParse(dto.start_time, out TimeOnly startTime) || !TimeOnly.TryParse(dto.end_time, out TimeOnly endTime))
            {
                return BadRequest("Invalid time format: Start time or end time is not a valid TimeOnly.");
            }

            if (startTime > endTime)
            {
                return BadRequest("Start time must be less than or equal to end time.");
            }

            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    //更新場館審核狀態
                    var gym = await _context.TGyms.FindAsync(id);
                    if (gym == null)
                    {
                        return NotFound();
                    }

                    gym.GymStatus = dto.GymStatus;
                    // 更新公司審核狀態
                    var company = await _context.Tcompanies.FindAsync(gym.CompanyId);
                    if (company != null)
                    {
                        company.Status = dto.GymStatus;
                    }
                    // 更新負責人審核狀態
                    var owner = await _context.Towners.FindAsync(company?.OwnerId);
                    if (owner != null)
                    {
                        owner.Status = dto.GymStatus;
                    }

                    //新增場地時間
                    var gymTimes = _context.TtimesDetails
               .Where(t => t.TimeName >= startTime && t.TimeName <= endTime)
               .Select(t => new TGymTime
               {
                   GymId = id,
                   GymTime = t.TimeId
               });
                    _context.TGymTimes.AddRange(gymTimes);
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();

                    return NoContent();
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    // Log the exception (ex) as needed
                    return StatusCode(500, "An error occurred while updating the gym status.");
                }
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGym(int id)
        {
            // Retrieve the gym including its related fields
            var gym = await _context.TGyms
                .Include(x => x.Tfields)
                .FirstOrDefaultAsync(x => x.GymId == id);

            if (gym == null)
            {
                return NotFound();
            }

            // Start a transaction
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    // Delete related gym times
                    var gymTimes = _context.TGymTimes.Where(gt => gt.GymId == id);
                    _context.TGymTimes.RemoveRange(gymTimes);

                    // Get related fields
                    var fields = _context.Tfields.Where(f => f.GymId == id).ToList();

                    // Delete related field photos
                    foreach (var field in fields)
                    {
                        var fieldPhotos = _context.TfieldPhotos.Where(fp => fp.FieldId == field.FieldId);
                        _context.TfieldPhotos.RemoveRange(fieldPhotos);
                    }

                    // Delete related fields
                    _context.Tfields.RemoveRange(fields);

                    // Delete the gym
                    _context.TGyms.Remove(gym);

                    // Save changes and commit the transaction
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();

                    return NoContent();
                }
                catch (Exception)
                {
                    // Rollback the transaction in case of an error
                    await transaction.RollbackAsync();
                    throw;
                }
            }
        }
    }
}
