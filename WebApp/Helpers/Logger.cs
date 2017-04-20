using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Helpers
{
    public static class Logger
    {
        private string logFileName = "";
        private string logFileDirectory = "";

        public string LogFileName
        {
            get
            {
                if (logFileName.Equals(""))
                {
                    logFileName = System.Configuration.ConfigurationManager.AppSettings.Get("ExceptionLogFileName");
                }

                return logFileName;
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