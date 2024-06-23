using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using projRESTfulApiFitConnect.Models;

namespace projRESTfulApiFitConnect.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileAddressController : ControllerBase
    {
        private readonly GymContext _db;
        private readonly IWebHostEnvironment _env;
        public ProfileAddressController(GymContext db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }
        //取得不重複縣市名稱
        [HttpGet("GetCity")]
        public async Task<IActionResult> GetCity()
        {
            var cities = await _db.Addresses.Select(x => x.City).Distinct().ToListAsync();
            return Ok(cities);
        }
        //依據選擇的縣市，取得不重複鄉鎮區
        [HttpGet("GetDistinction")]
        public async Task<IActionResult> GetDistinction(string city)
        {
            var distinctions = await _db.Addresses.Where(x => x.City.Equals(city)).Select(x => x.SiteId.Substring(3, x.SiteId.Length)).Distinct().ToListAsync();
            return Ok(distinctions);
        }
        //依據選擇的縣市及鄉鎮區，取得路名
        [HttpGet("GetRoad")]
        public async Task<IActionResult> GetRoad(string distinction)
        {
            var roads = await _db.Addresses.Where(x => x.SiteId.Equals(distinction)).Select(x => x.Road).Distinct().ToListAsync();
            return Ok(roads);
        }
        //依據選擇的縣市、鄉鎮區及路名，取得郵遞區號
        [HttpGet("GetZip")]
        public async Task<IActionResult> GetZip(string distinction,string road)
        {
            var zip = await _db.Addresses.Where(x => x.SiteId.Equals(distinction) && x.Road.Equals(road)).Select(x => x.Id).ToListAsync();
            return Ok(zip);
        }
    }
}
