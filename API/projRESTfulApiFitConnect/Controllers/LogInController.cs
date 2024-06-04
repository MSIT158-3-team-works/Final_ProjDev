using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using projRESTfulApiFitConnect.DTO.LogIn;
using projRESTfulApiFitConnect.Models;

namespace projRESTfulApiFitConnect.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogInController : ControllerBase
    {
        GymContext _context;
        public LogInController(GymContext context)
        {
            _context = context;
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromForm]PostIdentityDto postIdentityDto)
        {
            var identity =await _context.TIdentities.FirstOrDefaultAsync(x => (x.Phone == postIdentityDto.username || x.EMail == postIdentityDto.username) && x.Password == postIdentityDto.password);
            var result = new
            {
                identity.Id,
                identity.RoleId
            };
            return Ok(result);
        }
    }
}
