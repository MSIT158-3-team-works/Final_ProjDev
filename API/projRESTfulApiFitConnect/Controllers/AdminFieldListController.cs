using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using projRESTfulApiFitConnect.DTO.Gym;
using projRESTfulApiFitConnect.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace projRESTfulApiFitConnect.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminFieldListController : ControllerBase
    {
        private readonly GymContext _context;
        private readonly IWebHostEnvironment _env;

        public AdminFieldListController(GymContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        // GET: api/<AdminFieldListController>
        [HttpGet(Name = "GetFieldList")]
        public async Task<ActionResult<IEnumerable<FieldReviewDetailDto>>> GetFieldsList()
        {
            if (_context.TGyms == null)
            {
                return NotFound();
            }
            var fields = await _context.Tfields
        .Include(x => x.Gym)
        .Include(x => x.TfieldPhotos)
        .ToListAsync();

            var fieldDtos = new List<FieldReviewDetailDto>();

            foreach (var item in fields)
            {
                string base64Image = "";

                // Ensure TfieldPhotos is not null or empty
                if (item.TfieldPhotos != null && item.TfieldPhotos.Any())
                {
                    // Assuming you want the first photo in TfieldPhotos
                    var firstPhoto = item.TfieldPhotos.First();
                    if (firstPhoto != null)
                    {
                        // Construct the file path
                        string fileName = firstPhoto.FieldPhoto;
                        if (!string.IsNullOrEmpty(fileName))
                        {
                            string filepath = Path.Combine(_env.ContentRootPath, "Images", "FieldImages", fileName);
                            if (System.IO.File.Exists(filepath))
                            {
                                byte[] bytes = await System.IO.File.ReadAllBytesAsync(filepath);
                                base64Image = Convert.ToBase64String(bytes);
                            }
                        }
                    }
                }

                var fieldDto = new FieldReviewDetailDto()
                {
                    FieldId = item.FieldId,
                    GymId = item.GymId,
                    GymName = item.Gym.GymName,
                    FieldName = item.FieldName,
                    Floor = item.Floor,
                    FieldPayment = item.FieldPayment,
                    FieldDescribe = item.FieldDescribe,
                    FieldPhoto = base64Image,
                    Status = item.Status
                };

                fieldDtos.Add(fieldDto);
            }
            return Ok(fieldDtos);
        }

        // GET api/<AdminFieldlistController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FieldReviewDetailDto>> GetFieldReviewDetail(int id)
        {
            if (_context.TGyms == null)
            {
                return NotFound();
            }

            var field = await _context.Tfields
                .Include(x => x.Gym)
                .Include(x => x.TfieldPhotos)
                .FirstOrDefaultAsync(x => x.FieldId == id);

            if (field == null)
            {
                return NotFound();
            }

            string base64Image = "";

            // Ensure TfieldPhotos is not null or empty
            if (field.TfieldPhotos != null && field.TfieldPhotos.Any())
            {
                // Assuming you want the first photo in TfieldPhotos
                var firstPhoto = field.TfieldPhotos.First();
                if (firstPhoto != null)
                {
                    // Construct the file path
                    string fileName = firstPhoto.FieldPhoto;
                    if (!string.IsNullOrEmpty(fileName))
                    {
                        string filepath = Path.Combine(_env.ContentRootPath, "Images", "FieldImages", fileName);
                        if (System.IO.File.Exists(filepath))
                        {
                            byte[] bytes = await System.IO.File.ReadAllBytesAsync(filepath);
                            base64Image = Convert.ToBase64String(bytes);
                        }
                    }
                }
            }

            var fieldDto = new FieldReviewDetailDto()
            {
                FieldId = field.FieldId,
                GymId = field.Gym.GymId,
                GymName = field.Gym.GymName,
                FieldName = field.FieldName,
                Floor = field.Floor,
                FieldPayment = field.FieldPayment,
                FieldDescribe = field.FieldDescribe,
                FieldPhoto = base64Image,
                Status = field.Status
            };

            return Ok(fieldDto);
        }
        // PUT api/<AdminFieldListController>
        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateFieldStatus(int id, [FromBody] FieldStatusUpdateDto dto)
        {
            var field = await _context.Tfields.FindAsync(id);
            if (field == null)
            {
                return NotFound();
            }

            field.Status = dto.Status;
            await _context.SaveChangesAsync();

            return NoContent();
        }
        // DELETE api/<AdminFieldListController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteField(int id)
        {
            var field = await _context.Tfields
            .Include(f => f.TfieldPhotos)  
            .FirstOrDefaultAsync(f => f.FieldId == id);

            if (field == null)
            {
                return NotFound();
            }

            // 刪除關聯照片
            _context.TfieldPhotos.RemoveRange(field.TfieldPhotos);
            _context.Tfields.Remove(field);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }

            return NoContent();
        }
    }
}
