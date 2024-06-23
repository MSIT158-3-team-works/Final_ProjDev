using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using projRESTfulApiFitConnect.DTO.Member.comment;
using projRESTfulApiFitConnect.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace projRESTfulApiFitConnect.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly GymContext _context;
        private readonly IWebHostEnvironment _env;

        public CommentController(GymContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
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
                    RateId = rc.RateId,
                    ReserveId = rc.ReserveId,
                    MemberId = id,
                    ClassId = rc.ClassId,
                    CoachId = rc.CoachId,
                    RateClass = rc.RateClass,
                    RateClassDescribe = rc.ClassDescribe,
                    RateCoach = rc.RateCoach,
                    RateCoachDescribe = rc.CoachDescribe
                }).ToList();
                var coach = await _context.TIdentities.FindAsync(item.ClassSchedule.CoachId);
                //var theCoach = _context.TIdentities.FindAsync(item.ClassSchedule.CoachId);
                //var coach = _context.TIdentities.Where(x => x.Id == rates[0].CoachId).FirstOrDefault();
                CommentDetailDTO commentDetailDTO = new CommentDetailDTO
                {
                    ClassName = item.ClassSchedule.Class.ClassName,
                    Coach = item.ClassSchedule.CoachId,
                    ClassReserveId = item.ReserveId,
                    CoachName = coach.Name,
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
        public async Task<ActionResult<RateDetailDTO>> GetRate(int reserveId)
        {
            string base64Image0 = "";
            string base64Image = "";
            var rate = await _context.TmemberRateClasses
                                       .Include(x=>x.Reserve.ClassSchedule.Class)
                                     .FirstOrDefaultAsync(x => x.ReserveId == reserveId);
            if (rate == null)
            {
                return NotFound();
            }
            var theClass = _context.Tclasses.Where(x => x.ClassId == rate.ClassId).FirstOrDefault();
            if (!string.IsNullOrEmpty(theClass.ClassPhoto))
            {
                string path = Path.Combine(_env.ContentRootPath, "Images", "ClassPic", theClass.ClassPhoto);
                byte[] bytes = System.IO.File.ReadAllBytes(path);
                base64Image0 = Convert.ToBase64String(bytes);
            }
            var coach = _context.TIdentities.Where(x => x.Id == rate.CoachId).FirstOrDefault();
            if (!string.IsNullOrEmpty(coach.Photo))
            {
                string path = Path.Combine(_env.ContentRootPath, "Images", "CoachImages", coach.Photo);
                byte[] bytes = System.IO.File.ReadAllBytes(path);
                base64Image = Convert.ToBase64String(bytes);
            }

            var rateDto = new RateDetailDTO
            {
                RateId = rate.RateId,
                ReserveId = rate.ReserveId,
                MemberId = rate.MemberId,
                ClassId = rate.ClassId,
                ClassName = rate.Reserve.ClassSchedule.Class.ClassName,
                Classpic=base64Image0,
                CoachId = rate.CoachId,
                CoachName = coach.Name,
                Coachphoto=base64Image,
                RateClass = rate.RateClass,
                RateClassDescribe = rate.ClassDescribe,
                RateCoach = rate.RateCoach,
                RateCoachDescribe = rate.CoachDescribe
            };

            return Ok(rateDto);
        }

        // PUT: api/Comment/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("Rate/{id}")]
        public async Task<ActionResult<RatesDTO>> PutRates(int id, RateDTO rateDTO)
        {
            var classRate = await _context.TmemberRateClasses
                                 .Where(x => x.RateId == id)
                                 //.Include(cr => cr.ClassSchedule)
                                 .FirstOrDefaultAsync();

            if (classRate == null)
            {
                return NotFound("Class reserve not found.");
            }

            classRate.RateClass = rateDTO.RateClass ?? 0;
            classRate.ClassDescribe = rateDTO.RateClassDescribe;
            classRate.RateCoach = rateDTO.RateCoach ?? 0;
            classRate.CoachDescribe = rateDTO.RateCoachDescribe;

            // Set the state of the existing object to Modified
            _context.Entry(classRate).State = EntityState.Modified;

            //_context.Entry(rate).OriginalValues.SetValues(rate);
           // _context.Entry(rate).State = EntityState.Modified;
             await _context.SaveChangesAsync();

            return Ok("rate Edit success");

        }


        // POST: api/Comment
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<RatesDTO>> PostRates(/*[FromForm]*/ CommentDTO commentDTO)
        {
            var classReserve = await _context.TclassReserves
                                 //.Where(x => x.ReserveId == commentDTO.ReserveId)
                                 .Include(cr => cr.ClassSchedule)
                                 .FirstOrDefaultAsync(x => x.ReserveId == commentDTO.ReserveId);

            if (classReserve == null)
            {
                return NotFound("Class reserve not found.");
            }

            var rate = new TmemberRateClass
            {
                ReserveId = classReserve.ReserveId,
                MemberId = commentDTO.MemberId,
                ClassId = classReserve.ClassSchedule.ClassId,
                CoachId = classReserve.ClassSchedule.CoachId,

                RateClass = commentDTO.RateClass ?? 0,
                ClassDescribe = commentDTO.RateClassDescribe,
                RateCoach = commentDTO.RateCoach ?? 0,
                CoachDescribe = commentDTO.RateCoachDescribe
            };

            _context.TmemberRateClasses.Add(rate);
            await _context.SaveChangesAsync();

            // Return the newly created rate object
            return Ok(rate);

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
