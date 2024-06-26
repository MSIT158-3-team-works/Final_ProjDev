using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using projRESTfulApiFitConnect.DTO;
using projRESTfulApiFitConnect.DTO.Gym;
using projRESTfulApiFitConnect.Models;
using System;
using System.IO;


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
                    gymId = item.GymId,
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
        public async Task<IActionResult> PostTField([FromForm] FieldCreateDetailDto dto)
        {
            // 檢查GymId是否存在
            var gymExists = await _context.TGyms.AnyAsync(g => g.GymId == dto.GymId);
            if (!gymExists)
            {
                return BadRequest("Provided GymId does not exist.");
            }
            // 處理照片上傳
            string fieldPhotoFileName = null;
            if (dto.UploadedFieldPhoto != null && dto.UploadedFieldPhoto.Length > 0)
            {
                var uploads = Path.Combine(Directory.GetCurrentDirectory(), "Images", "FieldImages");
                if (!Directory.Exists(uploads))
                {
                    Directory.CreateDirectory(uploads);
                }

                fieldPhotoFileName = Path.GetFileName(dto.UploadedFieldPhoto.FileName);
                var filePath = Path.Combine(uploads, fieldPhotoFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await dto.UploadedFieldPhoto.CopyToAsync(fileStream);
                }
            }
            //新增場地
            Tfield newField = new Tfield
            {
                GymId = dto.GymId,
                FieldName = dto.FieldName,
                Floor = dto.Floor,
                FieldPayment = dto.FieldPayment,
                FieldDescribe = dto.FieldDescribe,
                Status = true
            };
            _context.Tfields.Add(newField);
            await _context.SaveChangesAsync();

            int fieldId = newField.FieldId;
            TfieldPhoto fieldPhoto = new TfieldPhoto
            {
                FieldId = fieldId,
                FieldPhoto = fieldPhotoFileName 
            };
            _context.TfieldPhotos.Add(fieldPhoto);
            await _context.SaveChangesAsync();

            return Ok(new { success = "field create success" });
        }
        // PUT api/<GymController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTfield(int id, [FromForm] FieldCreateDetailDto dto)
        {
            var existingField = await _context.Tfields
                .Include(x => x.TfieldPhotos)
                .FirstOrDefaultAsync(x => x.FieldId == id);

            if (existingField == null)
            {
                return NotFound();
            }

            // 更新場地資料
            existingField.GymId = dto.GymId;
            existingField.FieldName = dto.FieldName;
            existingField.Floor = dto.Floor;
            existingField.FieldPayment = dto.FieldPayment;
            existingField.FieldDescribe = dto.FieldDescribe;
            existingField.Status = true;

            // 照片更新
            if (dto.UploadedFieldPhoto != null && dto.UploadedFieldPhoto.Length > 0)
            {
                var uploads = Path.Combine(Directory.GetCurrentDirectory(), "Images", "FieldImages");
                if (!Directory.Exists(uploads))
                {
                    Directory.CreateDirectory(uploads);
                }

                var fieldPhotoFileName = Path.GetFileName(dto.UploadedFieldPhoto.FileName);
                var filePath = Path.Combine(uploads, fieldPhotoFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await dto.UploadedFieldPhoto.CopyToAsync(fileStream);
                }

                if (existingField.TfieldPhotos != null && existingField.TfieldPhotos.Any())
                {
                    existingField.TfieldPhotos.First().FieldPhoto = fieldPhotoFileName;
                }
                else
                {
                    TfieldPhoto fieldPhoto = new TfieldPhoto
                    {
                        FieldId = id,
                        FieldPhoto = fieldPhotoFileName
                    };
                    _context.TfieldPhotos.Add(fieldPhoto);
                }
            }
            else
            {
                // 保持原來的照片資料不變
                if (existingField.TfieldPhotos != null && existingField.TfieldPhotos.Any())
                {
                    var originalPhoto = existingField.TfieldPhotos.First().FieldPhoto;
                    // 確保不改變照片資料
                    _context.Entry(existingField.TfieldPhotos.First()).State = EntityState.Unchanged;
                    // 確保回傳原來的照片資料
                    existingField.TfieldPhotos.First().FieldPhoto = originalPhoto;
                }
            }

            await _context.SaveChangesAsync();

            return Ok(new { success = "field create success" });
        }
        private bool TfieldExists(int id)
        {
            return (_context.Tfields?.Any(e => e.FieldId == id)).GetValueOrDefault();
        }
    }
}
