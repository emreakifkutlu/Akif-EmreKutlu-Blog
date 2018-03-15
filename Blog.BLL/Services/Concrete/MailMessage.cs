using Blog.BLL.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Blog.BLL.Services.Concrete
{
    public class MailMessage : IMailMessage
    {
        public string To { get; set; }
        public bool SendMessage(string subject, string message)
        {
            System.Net.Mail.MailMessage ms = new System.Net.Mail.MailMessage();
            ms.From = new MailAddress("akif5353@gmail.com");
            ms.To.Add(To);
            ms.IsBodyHtml = true;
            ms.Body = message;
            ms.Subject = subject;

            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.EnableSsl = true;
            smtp.Credentials = new NetworkCredential("akif5353@gmail.com", "*");

            try
            {
                smtp.Send(ms);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
    }
}
