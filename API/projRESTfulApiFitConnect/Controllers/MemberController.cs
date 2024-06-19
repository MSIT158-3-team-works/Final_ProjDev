using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using projRESTfulApiFitConnect.Models;
using projRESTfulApiFitConnect.DTO.Member.status;
using projRESTfulApiFitConnect.DTO.Member;

namespace projRESTfulApiFitConnect.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberController : Controller
    {
        private readonly GymContext _context;
        private readonly IWebHostEnvironment _env;
        public MemberController(GymContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        [HttpGet]
        public IActionResult noreturn()
        {
            return NotFound();
        }

        [HttpGet("users/{id}")]
        public IActionResult getprofile(int? id)
        {
            //  get member by id
            if (id == null)
                return NotFound();

            if (!_context.TIdentities.Any(x => x.Id == id))
                return NotFound();

            var member = _context.TIdentities.Where(x => x.Id == id && x.Activated == true).Include(x => x.TcoachInfoIds).FirstOrDefault();
            string path = Path.Combine(_env.ContentRootPath, "Images", "MemberImages", "20240403154502.jpg");
            //string path = Path.Combine(_env.ContentRootPath, "Images", "MemberImages", member.Photo);
            byte[] bytes = System.IO.File.ReadAllBytes(path);
            member.Photo = Convert.ToBase64String(bytes);

            return Ok(member);
        }

        [HttpGet("others/{id}")]
        public IActionResult status(int? id)
        {
            //  get member's other datas by id
            if (id == null)
                return NotFound();

            if (!_context.TIdentities.Any(x => x.Id == id && x.Activated == true))
                return NotFound();

            MemberProfileDTO mp = new MemberProfileDTO((int)id, _context);
            var response = new
            {
                reserved = mp.li_reservedDetail,
                follow = mp.li_follow,
                comments = mp.li_rateClass,
            };
            if (mp.status)
                return Ok(response);
            return NotFound();
        }

        [HttpPost]
        public IActionResult Post([FromForm] AddMemberDto dto)
        {
            //  address null !!

            //  add new tidentity
            int r_id = 0;

            //  preprocessing photo
            string photo = "";

            //  preprocessing id for coach
            int p_id = -1;

            switch (r_id)
            {
                case 0:
                    TIdentity member = new TIdentity
                    {
                        RoleId = 1,
                        Name = dto.idName,
                        Phone = dto.idPhone,
                        EMail = dto.idEmail,
                        Password = dto.idPwd,
                        Photo = photo,
                        Birthday = dto.idBirthday,
                        Address = dto.address,
                        GenderId = dto.idGender,
                        Activated = true,
                        Payment = 0,
                    };
                    _context.TIdentities.Add(member);
                    _context.SaveChanges();
                    break;
                case 1:
                    bool isProcess = _context.TIdentities.Any(x => x.Id == p_id && x.RoleId == 1 && x.Activated == true);
                    if (!isProcess)
                        return NotFound();
                    TIdentity coach = _context.TIdentities.FirstOrDefault(x => x.Id == p_id && x.RoleId == 1 && x.Activated == true);
                    if (coach.Birthday.ToDateTime(TimeOnly.MinValue) < DateTime.Now.AddYears(-18))
                        return Ok("not old enouth");
                    if (coach.Payment != 0)
                        return Ok("isfine");
                    coach.RoleId = 2;
                    _context.SaveChanges();
                    break;
                default:
                    return NotFound();
            }
            return NotFound();
        }

        /*
        // POST: Member/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,RoleId,Name,Phone,EMail,Password,Photo,Birthday,Address,GenderId,Activated,Payment")] TIdentity tIdentity)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tIdentity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GenderId"] = new SelectList(_context.TgenderTables, "GenderId", "GenderId", tIdentity.GenderId);
            ViewData["RoleId"] = new SelectList(_context.TidentityRoleDetails, "RoleId", "RoleId", tIdentity.RoleId);
            return View(tIdentity);
        }

        // GET: Member/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tIdentity = await _context.TIdentities.FindAsync(id);
            if (tIdentity == null)
            {
                return NotFound();
            }
            ViewData["GenderId"] = new SelectList(_context.TgenderTables, "GenderId", "GenderId", tIdentity.GenderId);
            ViewData["RoleId"] = new SelectList(_context.TidentityRoleDetails, "RoleId", "RoleId", tIdentity.RoleId);
            return View(tIdentity);
        }

        // POST: Member/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,RoleId,Name,Phone,EMail,Password,Photo,Birthday,Address,GenderId,Activated,Payment")] TIdentity tIdentity)
        {
            if (id != tIdentity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tIdentity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TIdentityExists(tIdentity.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["GenderId"] = new SelectList(_context.TgenderTables, "GenderId", "GenderId", tIdentity.GenderId);
            ViewData["RoleId"] = new SelectList(_context.TidentityRoleDetails, "RoleId", "RoleId", tIdentity.RoleId);
            return View(tIdentity);
        }

        // GET: Member/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tIdentity = await _context.TIdentities
                .Include(t => t.Gender)
                .Include(t => t.Role)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tIdentity == null)
            {
                return NotFound();
            }

            return View(tIdentity);
        }

        // POST: Member/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tIdentity = await _context.TIdentities.FindAsync(id);
            if (tIdentity != null)
            {
                _context.TIdentities.Remove(tIdentity);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TIdentityExists(int id)
        {
            return _context.TIdentities.Any(e => e.Id == id);
        }
        */
    }
}
