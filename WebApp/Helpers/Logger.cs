using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Helpers
{
    public static class Logger
    {
        private static string exceptionLogFileName = "";
        private static string exceptionLogFileDirectory = "";
        private static string activityLogFileName = "";
        private static string activtyLogFileDirectory = "";

        public static string ExceptionLogFileName
        {
            get
            {
                if (exceptionLogFileName.Equals(""))
                {
                    exceptionLogFileName = System.Configuration.ConfigurationManager.AppSettings.Get("ExceptionLogFileName");
                }

                return exceptionLogFileName;
            }
        }

        public static string ExceptionLogFileDirectory
        {
            get
            {
                if (exceptionLogFileDirectory.Equals(""))
                {
                    exceptionLogFileDirectory = System.Configuration.ConfigurationManager.AppSettings.Get("ExceptionLogDirectory");
                }

                return exceptionLogFileDirectory;
            }
        }

        public static string ActivityLogFileName
        {
            get
            {
                if (activityLogFileName.Equals(""))
                {
                    activityLogFileName = System.Configuration.ConfigurationManager.AppSettings.Get("ActivityLogFileName");
                }

                return activityLogFileName;
            }
        }

        public static string ActivityLogFileDirectory
        {
            get
            {
                if (activtyLogFileDirectory.Equals(""))
                {
                    activtyLogFileDirectory = System.Configuration.ConfigurationManager.AppSettings.Get("ActivityLogDirectory");
                }

                return activtyLogFileDirectory;
            }
        }

        public static bool LogException()
        {
            bool success = false;

            try
            {

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