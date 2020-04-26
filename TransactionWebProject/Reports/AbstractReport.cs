using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace TransactionWebProject.Reports
{
    public enum ReportFileType
    {
        XLSX,
        XLS,
        HTML,
        TXT
    }
    public abstract class AbstractReport
    {
        public abstract void GetTxtReport();
        public string extentionPath =  @"D:\1.txt";
        public bool SendByEmail(ReportFileType fileType, string email)
        {
            if (fileType == ReportFileType.TXT) GetTxtReport();
            var taskEmailSender = Task.Run(() => SendEmailAsync(email));            
            return true;
        }
        private  async Task SendEmailAsync(string emailTo)
        {
            MailAddress from = new MailAddress("transactionDbTest@gmail.com", "Tom");
            MailAddress to = new MailAddress(emailTo);
            MailMessage m = new MailMessage(from, to);
            m.Subject = "Тест";
            m.Body = "Письмо-тест 2 работы smtp-клиента";

            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.Credentials = new NetworkCredential("transactionDbTest@gmail.com", "159753LL");
            smtp.EnableSsl = true;
            if (!string.IsNullOrEmpty(extentionPath))
            {
                m.Attachments.Add(new Attachment(extentionPath));
            }
            await smtp.SendMailAsync(m);
        }
    }
}
