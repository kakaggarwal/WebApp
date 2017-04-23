using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace WebApp.Helpers
{
    public static class Mailer
    {
        private static string _fromMailID = System.Configuration.ConfigurationManager.AppSettings.Get("FromMailID");
        private static string _fromMailDisplayName = System.Configuration.ConfigurationManager.AppSettings.Get("FromMailDisplayName");

        private static string _smtpHost = System.Configuration.ConfigurationManager.AppSettings.Get("smtpHost");
        private static string _smtpPort = System.Configuration.ConfigurationManager.AppSettings.Get("smtpPort");
        private static string _smtpUsername = System.Configuration.ConfigurationManager.AppSettings.Get("smtpUsername");
        private static string _smtpPassword = System.Configuration.ConfigurationManager.AppSettings.Get("smtpPassword");

        private static SmtpClient _smtpClient = null;

        private static MailAddress From
        {
            get
            {
                return new MailAddress(_fromMailID, _fromMailDisplayName);
            }
        }

        private static SmtpClient SmtpClient
        {
            get
            {
                if (_smtpClient == null)
                {
                    _smtpClient = ConstructSMTPClient();
                }

                return _smtpClient;
            }
        }

        public static bool SendMail(string subject, string body, string mailto, string mailtoname = "", List<Attachment> attachments = null)
        {
            bool success = false;

            try
            {
                MailAddress to = new MailAddress(mailto, mailtoname);
                MailMessage mailMessage;

                if (attachments == null)
                {
                    mailMessage = ConstructMailMessage(subject, body, to);
                }
                else
                {
                    mailMessage = ConstructMailMessage(subject, body, to, attachments);
                }

                if (mailMessage == null || SmtpClient == null)
                {
                    return false;
                }

                SmtpClient.Send(mailMessage);
                success = true;
            }
            catch (Exception ex)
            {
                success = false;
                Logger.LogException(ex);
            }

            return success;
        }

        private static MailMessage ConstructMailMessage(string subject, string body, MailAddress to, List<Attachment> attachments = null)
        {
            MailMessage mailMessage = new MailMessage(From, to);

            try
            {
                Logger.LogActivity("Commencing Mail Message Construction");

                mailMessage.Subject = subject;
                mailMessage.IsBodyHtml = true;
                mailMessage.Body = body;
                mailMessage.Priority = MailPriority.Normal;

                if (attachments != null)
                {
                    foreach (var attachment in attachments)
                    {
                        mailMessage.Attachments.Add(attachment);
                    }
                }

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
            SmtpClient smtpClient = new SmtpClient(_smtpHost);

            try
            {
                smtpClient.Port = Convert.ToInt32(_smtpPort);
                smtpClient.Credentials = new System.Net.NetworkCredential(_smtpUsername, _smtpPassword);
                smtpClient.EnableSsl = true;
            }
            catch (Exception ex)
            {
                smtpClient = null;
                Logger.LogException(ex);
            }

            return smtpClient;
        }
    }
}