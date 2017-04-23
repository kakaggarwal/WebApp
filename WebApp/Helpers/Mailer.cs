using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace WebApp.Helpers
{
    public static class Mailer
    {
        private static string fromMailID = System.Configuration.ConfigurationManager.AppSettings.Get("FromMailID");
        private static string fromMailDisplayName = System.Configuration.ConfigurationManager.AppSettings.Get("FromMailDisplayName");

        private static string smtpHost = System.Configuration.ConfigurationManager.AppSettings.Get("");
        private static string smtpPort = System.Configuration.ConfigurationManager.AppSettings.Get("");


        private static MailAddress From
        {
            get
            {
                return new MailAddress(fromMailID, fromMailDisplayName);
            }
        }

        public static bool SendMail(string subject, string body, string mailto, string mailtoname)
        {
            bool success = false;

            try
            {
                MailAddress to = new MailAddress(mailto, mailtoname);
                MailMessage mailMessage = ConstructMailMessage(subject, body, to);
                SmtpClient client = ConstructSMTPClient();

            }
            catch (Exception ex)
            {
                success = false;
                Logger.LogException(ex);
            }

            return success;
        }

        private static MailMessage ConstructMailMessage(string subject, string body, MailAddress to)
        {
            MailMessage mailMessage = new MailMessage(From, to);

            try
            {
                Logger.LogActivity("Commencing Mail Message Construction");

                mailMessage.Subject = subject;
                mailMessage.IsBodyHtml = true;
                mailMessage.Body = body;
                mailMessage.Priority = MailPriority.Normal;

                Logger.LogActivity("Mail Message Construction Complete");
            }
            catch (Exception ex)
            {
                mailMessage = null;
                Logger.LogException(ex);
            }

            return mailMessage;
        }

        private static SmtpClient ConstructSMTPClient()
        {
            SmtpClient smtpClient = new SmtpClient();

            try
            {
                smtpClient.Host = "";
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
        }
    }
}