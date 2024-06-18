using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using projRESTfulApiFitConnect.DTO;
using projRESTfulApiFitConnect.Models;


namespace projRESTfulApiFitConnect.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FieldInfoController : ControllerBase
    {
        private readonly GymContext _context;
        private readonly IWebHostEnvironment _env;

        public FieldInfoController(GymContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        // GET api/Field
        [HttpGet(Name = "GetFieldInfos")]
        public async Task<ActionResult<IEnumerable<FieldInfoDto>>> GetFieldsInfo()
        {
            string filepath = "";

            List<FieldInfoDto> fieldinfoDtos = new List<FieldInfoDto>();
            if (_context.TfieldPhotos == null)
            {
                return NotFound();
            }
            var fieldinfos = await _context.Tfields
                                .Where(x=>x.Status==true)
                                .Include(x => x.TfieldPhotos)
                                .ToListAsync();
            foreach (var item in fieldinfos)
            {
                string base64Image = "";

                var firstPhoto = item.TfieldPhotos.FirstOrDefault();
                if (firstPhoto != null)
                {
                    filepath = Path.Combine(_env.ContentRootPath, "Images", "FieldImages", firstPhoto.FieldPhoto);
                    if (System.IO.File.Exists(filepath))
                    {
                        byte[] bytes = await System.IO.File.ReadAllBytesAsync(filepath);
                        base64Image = Convert.ToBase64String(bytes);
                    }
                }

                FieldInfoDto fieldinfoDto = new FieldInfoDto()
                {
                    GymId = item.GymId,
                    FieldName = item.FieldName,
                    FieldPhoto = base64Image,
                    Status = item.Status
                };
                fieldinfoDtos.Add(fieldinfoDto);
            }
            return Ok(fieldinfoDtos);
        }

        // POST api/<FieldInfoController>
        [HttpGet("{id}")]
        public async Task<ActionResult<Tfield>> GetTField(int id)
        {
            if (_context.Tfields == null)
            {
                return NotFound();
            }
            var tField = await _context.Tfields.FindAsync(id);

            if (tField == null)
            {
                return NotFound();
            }

            return tField;
        }
    }
}
