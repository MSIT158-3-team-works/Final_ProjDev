using System.Text;
using System.Security.Cryptography;

namespace projRESTfulApiFitConnect.DTO.LogIn
{
	public class C_Session
    {
        private string _sessionId;
        public C_Session()
        {
            //  use DateTime.Now to be salt
            string salt = DateTime.Now.ToString("F");
            byte[] hash = Array.Empty<byte>();

            //  encript
            using (SHA256 encript = SHA256.Create())
            {
                encript.Initialize();
                encript.ComputeHash(Encoding.UTF8.GetBytes(salt));
                hash = encript.Hash;
            }

            StringBuilder hex = new StringBuilder(hash.Length * 2);
            foreach (byte b in hash)
                hex.AppendFormat("{0:x2}", b);
            _sessionId = hex.ToString();
        }

        public string getSessionId()
        {
            return _sessionId;
        }
    }
}
