﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using projRESTfulApiFitConnect.DTO.Product;
using projRESTfulApiFitConnect.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace projRESTfulApiFitConnect.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoachTrackController : ControllerBase
    {
        private readonly GymContext _context;
        private readonly IWebHostEnvironment _env;

        public CoachTrackController(GymContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }


        // GET: api/<CoachTrackController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<CoachTrackController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<CoachTrackController>
        [HttpPost]
        public async Task<ActionResult<TmemberFollow>> PostTmemberFollow(int memberId, int coachId)
        {
            TmemberFollow tmemberFollow = new TmemberFollow();
            tmemberFollow.StatusId = 1;
            tmemberFollow.MemberId = memberId;
            tmemberFollow.CoachId = coachId;
            _context.TmemberFollows.Add(tmemberFollow);
            await _context.SaveChangesAsync();

            return Ok();
        }

        // PUT api/<CoachTrackController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CoachTrackController>/5
        [HttpDelete("{memberId}/{coachId}")]
        public async Task<IActionResult> DeleteTcoachTrack(int memberId, int coachId)
        {
            var tcoachTrack = await _context.TmemberFollows.FirstOrDefaultAsync(f => f.MemberId == memberId && f.CoachId == coachId);
            if (tcoachTrack == null)
            {
                return NotFound();
            }

            _context.TmemberFollows.Remove(tcoachTrack);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        ///
        [HttpGet("check")]
        public async Task<IActionResult> CheckTmemberFollow(int memberId, int coachId)
        {
            var follow = await _context.TmemberFollows.FirstOrDefaultAsync(f => f.MemberId == memberId && f.CoachId == coachId);

            if (follow != null)
            {
                return Ok(new { exists = true });
            }

            return Ok(new { exists = false });
        }
    }
}
