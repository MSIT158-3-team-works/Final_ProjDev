using projRESTfulApiFitConnect.Models;

namespace projRESTfulApiFitConnect.DTO.Member
{
    public class MemberDto
    {
        private readonly GymContext _context;
        public MemberDto(GymContext context)
        {
            _context = context;
        }
        public int id { get; set; }
        //public int role_id
        //{
        //    get { return this.role_id; }
        //    set
        //        { 
        //            this.role_id = value;
        //            this.RoleDescription = _context.TidentityRoleDetails.Where(x => x.RoleId == this.role_id).FirstOrDefault().RoleDescribe;
        //        }
        //}
        public int g_id
        {
            get { return this.g_id; }
            set
            {
                this.g_id = value;
                this.GenderDescription = _context.TgenderTables.Where(x => x.GenderId == this.g_id).FirstOrDefault().GenderText;
            }
        }
        public string name { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string photo { get; set; }
        public DateOnly birthday { get; set; }
        public string address { get; set; }
        public double payment { get; set; }
        //public string RoleDescription { get; set; }
        public string GenderDescription { get; set; }
    }
}
