using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using projRESTfulApiFitConnect.DTO.Member;
using projRESTfulApiFitConnect.Models;

namespace projRESTfulApiFitConnect.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly GymContext _context;

        public CommentController(GymContext context)
        {
            _context = context;
        }

        // GET: api/Comment
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CommentDetailDTO>>> GetCommentDetail(int id)
        {
            List<CommentDetailDTO> CommentDetailDTOs = new List<CommentDetailDTO>();

            var Comments = await _context.TclassReserves
                        .Where(x => x.MemberId == id)
                        .Include(x => x.TmemberRateClasses)
                        .Include(x => x.Member)
                        .Include(x => x.ClassSchedule.Class)
                        .Include(x => x.ClassSchedule.Field.Gym)
                        .Include(x => x.ClassSchedule.CourseStartTime)
                        .ToListAsync();

            foreach (var item in Comments)
            {
                var rates = item.TmemberRateClasses.Select(rc => new RatesDTO
                {
                    RateClass = rc.RateClass,
                    RateClassDescribe = rc.ClassDescribe,
                    RateCoach = rc.RateCoach,
                    RateCoachDescribe = rc.CoachDescribe
                }).ToList();


                CommentDetailDTO commentDetailDTO = new CommentDetailDTO
                {
                    ClassName = item.ClassSchedule.Class.ClassName,
                    Coach = item.ClassSchedule.CoachId,
                    GymName = item.ClassSchedule.Field.Gym.GymName,
                    CourseDate = item.ClassSchedule.CourseDate,
                    CourseStartTime = item.ClassSchedule.CourseStartTime.TimeName,
                    Rates = rates,
                };
                CommentDetailDTOs.Add(commentDetailDTO);
            }

            return CommentDetailDTOs;
        }

        // GET: api/Comment/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TclassReserve>> GetTclassReserve(int id)
        {
            var tclassReserve = await _context.TclassReserves.FindAsync(id);

            if (tclassReserve == null)
            {
                return NotFound();
            }

            return tclassReserve;
        }

        // GET: api/Comment/Rate/{reserveId}
        [HttpGet("Rate/{reserveId}")]
        public async Task<ActionResult<RatesDTO>> GetRate(int reserveId)
        {
            var rate = await _context.TmemberRateClasses
                                     .FirstOrDefaultAsync(x => x.ReserveId == reserveId);
            if (rate == null)
            {
                return NotFound();
            }

            var rateDto = new RatesDTO
            {
                ReserveId = rate.ReserveId,
                MemberId = rate.MemberId,
                ClassId = rate.ClassId,
                CoachId = rate.CoachId,
                RateClass = rate.RateClass,
                RateClassDescribe = rate.ClassDescribe,
                RateCoach = rate.RateCoach,
                RateCoachDescribe = rate.CoachDescribe
            };

            return Ok(rateDto);
        }

        // PUT: api/Comment/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("Rate/{reserveId}")]
        public async Task<IActionResult> PutRate(int reserveId, [FromForm] RatesDTO dto)
        {
            //TmemberRateClass rate = _context.TmemberRateClasses.Where(x => x.ReserveId == dto.ReserveId).FirstOrDefault();
            var rate =await _context.TmemberRateClasses.FirstOrDefaultAsync(x => x.ReserveId == dto.ReserveId);
            if (rate == null)
            {
                return NotFound();
            }
            rate.ClassDescribe = dto.RateClassDescribe;
            //rate.RateClass = (decimal)dto.RateClass;
            rate.RateClass = dto.RateClass ?? 0;
            rate.CoachDescribe = dto.RateCoachDescribe;
            //rate.RateCoach = (decimal)dto.RateCoach;
            rate.RateCoach = dto.RateCoach ?? 0;
            //_context.SaveChanges();
            _context.TmemberRateClasses.Update(rate);
            await _context.SaveChangesAsync();

            return Ok("goodjob!!!");
        }





        // POST: api/Comment
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<RatesDTO>> PostRates([FromForm] RatesDTO ratesDTO)
        {
            // Fetch the tclass_reserve entry based on ReserveId
            var classReserve = await _context.TclassReserves
                                .Include(cr => cr.ClassSchedule)
                                .FirstOrDefaultAsync(cr => cr.ReserveId == ratesDTO.ReserveId);

            if (classReserve == null)
            {
                return NotFound("Class reserve not found.");
            }

            // Fetch ClassId and CoachId based on the fetched classReserve
            var classId = classReserve.ClassSchedule.ClassId;
            var coachId = classReserve.ClassSchedule.CoachId;

            var rate = new TmemberRateClass
            {
                //如何自動帶入資料?
                ReserveId = ratesDTO.ReserveId,
                MemberId = ratesDTO.MemberId,
                ClassId = classId,
                CoachId = coachId,

                RateClass = ratesDTO.RateClass ?? 0,
                ClassDescribe = ratesDTO.RateClassDescribe,
                RateCoach = ratesDTO.RateCoach ?? 0,
                CoachDescribe = ratesDTO.RateCoachDescribe
            };

            _context.TmemberRateClasses.Add(rate);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTclassReserve), new { id = rate.ReserveId }, ratesDTO);

            //List<TclassReserve> crs = new List<TclassReserve>();
            //var Comments = await _context.TclassReserves
            //            .Where(x => x.MemberId == ratesDTO.MemberId)
            //            //.Include(x=>x.ClassSchedule)
            //            //.Include(x => x.TmemberRateClasses)
            //            //.Include(x => x.Member)
            //            //.Include(x => x.ClassSchedule.Class)
            //            //.Include(x => x.ClassSchedule.Field.Gym)
            //            //.Include(x => x.ClassSchedule.CourseStartTime)
            //            .ToListAsync();
            //for (var i = 0; i < Comments.Count; i++) { 
            //    var rId = Comments[i].ReserveId;
            //    var bag = await _context.TclassReserves
            //            .Include(x => x.ClassScheduleId)
            //            .ToListAsync();
            //    crs.Add(bag);
            //}


            /*TmemberRateClass rate = new TmemberRateClass();
            //todo:自動帶入資料
            rate.ReserveId = ratesDTO.ReserveId;
            rate.MemberId = ratesDTO.MemberId;
            rate.ClassId = ratesDTO.ClassId;
            rate.CoachId = ratesDTO.CoachId;
            //可自定義
            rate.RateClass= (decimal)ratesDTO.RateClass;
            rate.ClassDescribe = ratesDTO.RateClassDescribe;
            rate.RateCoach = (decimal)ratesDTO.RateCoach;
            rate.CoachDescribe = ratesDTO.RateCoachDescribe;


            _context.TmemberRateClasses.Add(rate);
            await _context.SaveChangesAsync();*/
            //return Ok(Comments);
            //return CreatedAtAction("GetTRates", new { id = rate.ReserveId }, ratesDTO);
        }

        //DELETE: api/Comment/5
        [HttpDelete("{id}")]//id是reserveid
        public async Task<IActionResult> DeleteRate(int id)
        {
            var rate = await _context.TmemberRateClasses.FindAsync(id);
            if (rate == null)
            {
                return NotFound();
            }

            _context.TmemberRateClasses.Remove(rate);
            await _context.SaveChangesAsync();

            return NoContent();
            //    var tclassReserve = await _context.TmemberRateClasses.FirstOrDefaultAsync();
            //    if (tclassReserve == null)
            //    {
            //        return NotFound();
            //    }

            //    _context.TclassReserves.Remove(tclassReserve);
            //    await _context.SaveChangesAsync();

            //    return NoContent();
        }

            private bool TclassReserveExists(int id)
        {
            return _context.TclassReserves.Any(e => e.ReserveId == id);
        }
    }
}
