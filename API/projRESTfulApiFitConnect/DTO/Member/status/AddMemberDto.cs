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
    }
}