using System;
using System.Net.Mail;
using System.Web;

namespace Core.Utility
{
    public class Email
    {
        private string messageSubject;
        private string messageBody;
        private string emailTo;
        private string emailFrom;
        private HttpPostedFile emailAttachment;
        public bool isSuccess { get; set; }
        public string exMessage { get; set; }
        public Email(string messageSubject, string messageBody, string emailFrom, string emailTo, HttpPostedFile emailAttachment = null)
        {
            this.messageSubject = messageSubject;
            this.messageBody = messageBody;
            this.emailTo = emailTo;
            this.emailFrom = emailFrom;
            this.emailAttachment = emailAttachment;
        }

        private MailMessage MakeEmail()
        {
            MailMessage message = new MailMessage(emailFrom, emailTo);
            message.IsBodyHtml = true;
            message.Subject = this.messageSubject;
            message.Body = this.messageBody;
            if (emailAttachment != null)
            {
                Attachment attachment = new Attachment(emailAttachment.InputStream, emailAttachment.FileName);
                message.Attachments.Add(attachment);
            }
            return message;
        }

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
               exMessage =ex.Message;
            }
        }

    }
}
