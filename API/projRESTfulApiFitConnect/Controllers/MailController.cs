using Microsoft.AspNetCore.Mvc;
using projRESTfulApiFitConnect.DTO.Mail;
using System.Net;
using System.Net.Mail;

namespace projRESTfulApiFitConnect.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MailController : Controller
    {
        [HttpGet]
        public IActionResult sendMail([FromForm] MailDTO dto)
        {
            if (toSend(dto) == 0)
                return Ok();

            return BadRequest();
        }

        [HttpGet("default")]
        public IActionResult defaultMail()
        {
            if (toSend(new MailDTO()) == 0)
                return Ok();

            return BadRequest();
        }

        private int toSend(MailDTO dto)
        {
            string user = ""; string link = "";

            string to = dto.toMail;
            string from = "fitconnectoffical@gmail.com";
            string subject = "FitConnect註冊登入";
            string body = $@"<p>{user}您好：<br /><span>&emsp;&emsp;</span>您於{DateTime.Now.ToString("G")}利用此信箱，於FitConnect註冊並登入，若要啟用您的帳號，請點選以下連結{link}。<br /><span>&emsp;&emsp;</span>若您對此沒有印象，請忽略此信，並檢查帳號登入紀錄，謝謝。<br /><hr /></p><p>2024&emsp;FitConnect&copy;&emsp;All Right Reserved.</p>";

            string smtpServer = "smtp.gmail.com";
            int smtpPort = 587;
            string smtpUsername = "fitconnectoffical@gmail.com";
            string smtpPassword = "gypafoyvggpfiglk";

            // 建立 MailMessage 物件,設定郵件格式為 HTML
            MailMessage mail = new MailMessage(from, to, subject, body);
            mail.IsBodyHtml = true;

            // 建立 SmtpClient 物件並設定 SMTP 伺服器資訊
            SmtpClient smtpClient = new SmtpClient(smtpServer, smtpPort);
            smtpClient.EnableSsl = true; // 使用 SSL 加密連線
            smtpClient.UseDefaultCredentials = false; // 不使用預設認證，需要手動設定帳號密碼
            smtpClient.Credentials = new NetworkCredential(smtpUsername, smtpPassword);

            try
            {
                smtpClient.Send(mail);
                Console.WriteLine("mail sended");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"寄送郵件時發生錯誤: {ex.Message}");
                return -1;
            }
            finally
            {
                mail.Dispose();
                smtpClient.Dispose();
            }

            return 0;
        }
    }
}
