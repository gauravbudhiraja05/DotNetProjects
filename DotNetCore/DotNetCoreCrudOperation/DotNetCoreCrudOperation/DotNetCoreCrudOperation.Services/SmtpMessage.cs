using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using PickfordsIntranet.Core.ThirdPartyServices;

namespace PickfordsIntranet.Services
{
    /// <summary>
    /// SmtpMessage implemented IMessage interface to send mail
    /// </summary>
    public class SmtpMessage : IMessage
    {
        private string _body;
        private bool _bodyIsHtml;
        private string _fromEmail;
        private string _fromName;
        private string _password;
        private string _userName;
        private Core.ThirdPartyServices.MessagePriority _priority;
        private string _replyToEmail;
        private string _replyToName;
        private string _subject;
        private List<Recipient> _recipients;
        private List<Recipient> _ccRecipients;
        private List<Recipient> _bccRecipients;
        private IConfigurationRoot _config;
        private string _smtpServer;
        private int _smtpServerPort;

        public SmtpMessage(IConfigurationRoot config)
        {
            _config = config;
            _smtpServer = _config["MailSettings:ServerAddress"];
            Int32.TryParse(_config["MailSettings:ServerPort"], out _smtpServerPort);

            _body = "";
            _fromEmail = _config["MailSettings:FromEmail"];
            _fromName = _config["MailSettings:FromName"];
            _priority = Core.ThirdPartyServices.MessagePriority.Normal;
            _replyToEmail = "";
            _replyToName = "";
            _subject = "";
            _recipients = new List<Recipient>();
            _ccRecipients = new List<Recipient>();
            _bccRecipients = new List<Recipient>();
            _password = _config["MailSettings:Password"];
            _userName = _config["MailSettings:UserName"];
        }

       

        public string Body
        {
            get
            {
                return _body;
            }

            set
            {
                _body = value;
            }
        }

        public bool BodyIsHtml
        {
            get
            {
                return _bodyIsHtml;
            }

            set
            {
                _bodyIsHtml = value;
            }
        }

        public string FromEmail
        {
            get
            {
                return _fromEmail;
            }

            set
            {
                _fromEmail = value;
            }
        }

        public string FromName
        {
            get
            {
                return _fromName;
            }

            set
            {
                _fromName = value;
            }
        }

        public Core.ThirdPartyServices.MessagePriority Priority
        {
            get
            {
                return _priority;
            }

            set
            {
                _priority = value;
            }
        }

        public string ReplyToEmail
        {
            get
            {
                return _replyToEmail;
            }

            set
            {
                _replyToEmail = value;
            }
        }

        public string ReplyToName
        {
            get
            {
                return _replyToName;
            }

            set
            {
                _replyToName = value;
            }
        }

        public string Subject
        {
            get
            {
                return _subject;
            }

            set
            {
                _subject = value;
            }
        }

        public IEnumerable<Recipient> Recipients
        {
            get
            {
                return _recipients;
            }
        }

        public IEnumerable<Recipient> CCRecipients
        {
            get
            {
                return _ccRecipients;
            }
        }

        public IEnumerable<Recipient> BCCRecipients
        {
            get
            {
                return _bccRecipients;
            }
        }

        Core.ThirdPartyServices.MessagePriority IMessage.Priority
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public void AddBCCRecipient(string fullName, string emailAddress)
        {
            _bccRecipients.Add(new Recipient(fullName, emailAddress));
        }

        public void AddCCRecipient(string fullName, string emailAddress)
        {
            _ccRecipients.Add(new Recipient(fullName, emailAddress));
        }

        public void AddRecipient(string fullName, string emailAddress)
        {
            _recipients.Add(new Recipient(fullName, emailAddress));
        }

        public void RemoveAllRecipaints()
        {
            _recipients.ForEach(r => { _recipients.Remove(r); });
        }
        public async Task<bool> Send()
        {

            var mimeMessage = new MimeMessage();


            mimeMessage.From.Add(new MailboxAddress(_fromName, _fromEmail));

            foreach (Recipient rec in _recipients)
            {
                mimeMessage.To.Add(new MailboxAddress(rec.FullName, rec.Email));
            }

            foreach (Recipient rec in _ccRecipients)
            {
                mimeMessage.Cc.Add(new MailboxAddress(rec.FullName, rec.Email));
            }

            foreach (Recipient rec in _bccRecipients)
            {
                mimeMessage.Bcc.Add(new MailboxAddress(rec.FullName, rec.Email));
            }

            mimeMessage.Subject = _subject;

            if (_bodyIsHtml)
            {
                mimeMessage.Body = new TextPart("html") { Text = _body };
            }
            else
            {
                mimeMessage.Body = new TextPart("plain") { Text = _body };
            }
            
            using (var smtpClient = new SmtpClient())
            {
                try
                {
                    //smtpClient.LocalDomain = "epicreg.com";
                    var temp = SecureSocketOptions.StartTlsWhenAvailable;
                    await smtpClient.ConnectAsync(_smtpServer, _smtpServerPort, temp).ConfigureAwait(false);
                    smtpClient.Authenticate(_userName, _password);
                    await smtpClient.SendAsync(mimeMessage).ConfigureAwait(false);
                    await smtpClient.DisconnectAsync(true).ConfigureAwait(false);
                }
                catch(Exception ex)
                {
                    Console.Write(ex.Message);
                }
            }
            RemoveAllRecipaints();
            return true;
        }
    }
}
