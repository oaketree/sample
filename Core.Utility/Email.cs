using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Web;

namespace Core.Utility
{
    public abstract class Email
    {
        protected string messageSubject;
        protected string messageBody;
        //private string emailTo;
        //private List<string> mailToArray;
        //private List<string> mailCcArray;
        protected string emailFrom;
        protected List<HttpPostedFile> emailAttachments;
        //protected HttpPostedFile emailAttachment;
        public bool isSuccess { get; set; }
        public string exMessage { get; set; }
        public Email(string messageSubject, string messageBody, string emailFrom, List<HttpPostedFile> emailAttachments = null)
        {
            this.messageSubject = messageSubject;
            this.messageBody = messageBody;
            this.emailFrom = emailFrom;
            this.emailAttachments = emailAttachments;
        }
        protected abstract MailMessage MakeEmail();


        public void send(string smtp, string username, string password)
        {
            //SmtpClient Client = new SmtpClient("smtp.126.com");
            SmtpClient Client = new SmtpClient(smtp);
            Client.UseDefaultCredentials = false;
            //Client.Credentials = new System.Net.NetworkCredential("shgygl", "bjb142900");
            Client.Credentials = new System.Net.NetworkCredential(username, password);
            Client.DeliveryMethod = SmtpDeliveryMethod.Network;
            try
            {
                Client.Send(MakeEmail());
                isSuccess = true;
            }
            catch (Exception ex)
            {
                isSuccess = false;
                exMessage = ex.Message;
            }
        }

        public Tuple<bool, string> Result
        {
            get
            {
                return Tuple.Create(isSuccess, exMessage);
            }
        }
    }

    public class DEmail : Email
    {
        private string emailTo;
        public DEmail(string messageSubject, string messageBody, string emailFrom, string emailTo, List<HttpPostedFile> emailAttachments = null) :base(messageSubject, messageBody, emailFrom, emailAttachments)
        {
            this.emailTo = emailTo;
        }
        protected override MailMessage MakeEmail()
        {
            MailMessage message = new MailMessage(emailFrom, emailTo);
            message.IsBodyHtml = true;
            message.Subject = this.messageSubject;
            message.Body = this.messageBody;
            if (emailAttachments != null && emailAttachments.Count > 0)
            {
                foreach (HttpPostedFile emailAttachment in emailAttachments)
                {
                    if (emailAttachment.FileName != string.Empty)
                    {
                        Attachment attachment = new Attachment(emailAttachment.InputStream, emailAttachment.FileName);
                        message.Attachments.Add(attachment);
                    }
                }
            }
            return message;
        }
    }
    public class QEmail : Email
    {
        private List<string> mailToArray;
        public QEmail(string messageSubject, string messageBody, string emailFrom, List<string> mailToArray, List<HttpPostedFile> emailAttachments = null) :base(messageSubject, messageBody, emailFrom, emailAttachments)
        {
            this.mailToArray = mailToArray;
        }

        protected override MailMessage MakeEmail()
        {
            var maddr = new MailAddress(emailFrom);
            var message = new MailMessage();
            if (mailToArray.Count > 0) {
                foreach (var item in mailToArray) {
                    message.To.Add(item);
                }
            }
            message.From = maddr;
            message.IsBodyHtml = true;
            message.Subject = this.messageSubject;
            message.Body = this.messageBody;
            if (emailAttachments != null && emailAttachments.Count > 0)
            {
                foreach (HttpPostedFile emailAttachment in emailAttachments)
                {
                    if (emailAttachment.FileName != string.Empty)
                    {
                        Attachment attachment = new Attachment(emailAttachment.InputStream, emailAttachment.FileName);
                        message.Attachments.Add(attachment);
                    }
                }
            }
            return message;
        }
    }
}
