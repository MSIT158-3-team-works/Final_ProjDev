using System.Data.SqlTypes;

namespace projRESTfulApiFitConnect.DTO.Member
{
    public class AddMemberDto
    {
        public string idName { get; set; }
        public string idPhone { get; set; }
        public string? idEmail { get; set; }
        public DateOnly idBirthday { get; set; }
        public string idPwd { get; set; }
        public int idGender { get; set; }
        public string photo { get; set; } = null;
        public string address { get; set; } = null;
        public string g_desc { get; set; } = null;
    }
}