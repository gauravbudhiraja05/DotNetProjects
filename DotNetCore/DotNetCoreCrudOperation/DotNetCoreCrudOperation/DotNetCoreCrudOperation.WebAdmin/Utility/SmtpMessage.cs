using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace PickfordsIntranet.WebAdmin.Utility
{
    public class SmtpMessage
    {
        private IConfiguration _config;
        private string _smtpServer = null;
        private Int16 _smtpPort = 0;
        private string _fromAddress = null;
        private string _password = null;
        public string Subject { get; set; }
        public string BodyContent { get; set; }
        public string ToAddress { get; set; }

        public SmtpMessage(IConfigurationRoot config)
        {
            _config = config;
            _smtpServer = _config["MailSettings:ServerAddress"];
            Int16.TryParse(_config["MailSettings:ServerPort"], out _smtpPort);
            _fromAddress = _config["MailSettings:FromEmail"];
            _password= _config["MailSettings:Password"];
        }

        public bool Send()
        {
            try
            {
                MailMessage mail = new MailMessage();
                string[] mailToArray = ToAddress.Split(';');
                Array.ForEach(mailToArray, (mailTo) =>
                {
                    if (!string.IsNullOrEmpty(mailTo))
                    {
                        mail.To.Add(mailTo);
                    }
                });
               
                mail.From = new MailAddress(_fromAddress);
                mail.Subject = Subject;
                mail.Body = BodyContent;
                mail.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient(_smtpServer, _smtpPort);
                smtp.EnableSsl = false;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new System.Net.NetworkCredential(_fromAddress, _password);
                smtp.Send(mail);
                return true;
            }

            catch(Exception ex)
            {
                throw ex;
            }
        }

        public bool SendAsync()
        {
            try
            {
                MailMessage mail = new MailMessage();
                string[] mailToArray = ToAddress.Split(';');
                Array.ForEach(mailToArray, (mailTo) =>
                {
                    if (!string.IsNullOrEmpty(mailTo))
                    {
                        mail.To.Add(mailTo);
                    }
                });
                mail.Bcc.Add("arunodaya.kumar@idslogic.com");
                mail.From = new MailAddress(_fromAddress);
                mail.Subject = Subject;
                mail.Body = BodyContent;
                mail.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient(_smtpServer, _smtpPort);
                smtp.EnableSsl = false;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new System.Net.NetworkCredential(_fromAddress, _password);
                smtp.SendAsync(mail,null);
                return true;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
