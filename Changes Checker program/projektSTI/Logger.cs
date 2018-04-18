using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;

namespace ProjektSTI
{
    class Logger
    {

        public void OdesliEmail()
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                mail.From = new MailAddress("mail_from");
                mail.To.Add("mail_to");
                mail.Subject = "Test Mail";
                mail.Body = "zprava";

                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential("login", "password");
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);
            }
            catch (Exception ex)
            {
                //throw;
            }
        }

    }
}