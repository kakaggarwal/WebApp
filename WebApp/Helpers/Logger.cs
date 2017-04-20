using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Web;
using System.Web.Hosting;

namespace WebApp.Helpers
{
    public static class Logger
    {
        private static string exceptionLogFileName = System.Configuration.ConfigurationManager.AppSettings.Get("ExceptionLogFileName");
        private static string exceptionLogFileDirectory = System.Configuration.ConfigurationManager.AppSettings.Get("ExceptionLogDirectory");
        private static string activityLogFileName = System.Configuration.ConfigurationManager.AppSettings.Get("ActivityLogFileName");
        private static string activtyLogFileDirectory = System.Configuration.ConfigurationManager.AppSettings.Get("ActivityLogDirectory");

        public static bool LogException(Exception exception)
        {
            bool success = false;

            try
            {
                string serverPath = HostingEnvironment.MapPath(exceptionLogFileDirectory);

                if (Directory.Exists(serverPath))
                {
                    Directory.CreateDirectory(serverPath);
                }

                using (StreamWriter streamWriter = File.AppendText(serverPath + exceptionLogFileName))
                {
                    streamWriter.WriteLine("*************************** Log Entry Begin *************************************");
                    streamWriter.WriteLine("Log Entry: " + DateTime.Now);
                    streamWriter.WriteLine("Exception:");
                    streamWriter.WriteLine("Message: " + exception.Message);
                    streamWriter.WriteLine("Source: " + exception.Source);
                    streamWriter.WriteLine("StackTrace: " + exception.StackTrace);

                    exception = exception.InnerException;

                    while (exception != null)
                    {
                        streamWriter.WriteLine("InnerException:");
                        streamWriter.WriteLine("Message: " + exception.Message);
                        streamWriter.WriteLine("Source: " + exception.Source);
                        streamWriter.WriteLine("StackTrace: " + exception.StackTrace);

                        exception = exception.InnerException;
                    }

                    streamWriter.WriteLine("*************************** Log Entry Ends *************************************");
                }
            }
            catch(Exception ex)
            {
                success = false;
                string fault = ex.Message;
            }

            return success;
        }
    }
}