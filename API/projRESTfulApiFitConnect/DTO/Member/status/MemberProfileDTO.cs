using Microsoft.EntityFrameworkCore;
using projRESTfulApiFitConnect.Models;

namespace projRESTfulApiFitConnect.DTO.Member.status
{
    public class MemberProfileDTO
    {
        private readonly GymContext _context;
        public MemberProfileDTO(int id, GymContext context)
        {
            _context = context;
            if (!_context.TIdentities.Any(x => x.Id == id && x.RoleId == 1 && x.Activated == true))
                return;

            //this.li_reservedDetail = _context.ClassReservedDetails.Where(x => x.MemberId == id).ToList();
            this.li_follow = _context.TmemberFollows.Where(x => x.MemberId == id).ToList();
            //this.li_follow = _context.TmemberFollows.Where(x => x.MemberId == id).Include(x => x.Status).ToList();
            this.li_rateClass = _context.TmemberRateClasses.Where(x => x.MemberId == id).ToList();

            //var member = _context.TIdentities.Where(x => x.Id == id).Include(x => x.TcoachInfoIds).FirstOrDefault();

            this.status = this.li_follow != null && this.li_rateClass != null;
        }

        public bool status { get; set; } = false;
        //public List<ClassReservedDetail> li_reservedDetail { get; set; } = null;
        public List<TmemberFollow>? li_follow { get; set; } = null;
        public List<TmemberRateClass> li_rateClass { get; set; } = null;
    }
}
