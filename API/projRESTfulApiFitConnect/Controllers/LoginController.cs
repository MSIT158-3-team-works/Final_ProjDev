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
		List<string> list = new List<string>();
		GymContext _context;
		public LogInController(GymContext context)
		{
			_context = context;
		}
		[HttpPost]
		public async Task<IActionResult> Post(PostIdentityDto postIdentityDto)
		{
			//string response = "notfound";
			var response = new
			{
				text = "notfound",
			};

			bool isExist = await _context.TIdentities.AnyAsync(x => (x.Phone == postIdentityDto.username || x.EMail == postIdentityDto.username));
			if (!isExist)
				return Ok(response);

			var identity = await _context.TIdentities.FirstOrDefaultAsync(x => (x.Phone == postIdentityDto.username || x.EMail == postIdentityDto.username) && x.Password == postIdentityDto.password);
			//response = "pswwrong";
            response = new
            {
                text = "pswwrong",
            };
            if (identity == null)
				return Ok(response);

            var result = new
			{
				identity.Id,
				identity.RoleId
			};

			return Ok(result);
		}
	}
}
