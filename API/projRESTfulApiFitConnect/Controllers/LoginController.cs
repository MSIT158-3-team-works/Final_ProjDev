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
        private static List<int> users = new List<int>();

        GymContext _context;
        public LogInController(GymContext context)
        {
            _context = context;
        }
        [HttpPost]
        public async Task<IActionResult> login(PostIdentityDto postIdentityDto)
        {
            //	lookup login users
            var response = new
            {
                text = "notfound",
            };

            bool isExist = await _context.TIdentities.AnyAsync(x => (x.Phone == postIdentityDto.username || x.EMail == postIdentityDto.username));
            if (!isExist)
                return Ok(response);

            var identity = await _context.TIdentities.FirstOrDefaultAsync(x => (x.Phone == postIdentityDto.username || x.EMail == postIdentityDto.username) && x.Password == postIdentityDto.password);
            response = new
            {
                text = "pswwrong",
            };
            if (identity == null)
                return Ok(response);

            C_account user = new C_account
            {
                id = identity.Id,
                role_id = identity.RoleId,
            };
            users.Add(identity.Id);

            return Ok(user);
        }

        [HttpGet("{id}")]
        public IActionResult isUserLogin(int? id)
        {
            //	chekcup is this user login or not
            if (id == null)
                return NotFound();

            if (users.Contains((int)id))
                return Ok();

            return NotFound();
        }

        [HttpDelete]
        public IActionResult logout(int? id)
        {
            //	user logout
            if (id == null)
                return NotFound();

            if (users.Contains((int)id))
            {
                users.Remove((int)id);
                return Ok();
            }

            return NotFound();
        }
    }
}
