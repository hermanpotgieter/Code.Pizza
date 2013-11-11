using System;
using System.Configuration;
using System.Web.Security;

namespace Code.Pizza.Web
{
    public static class ConfigSettings
    {
        public static int RememberMeCookieDays
        {
            get
            {
                int rememberMeCookieDays = Convert.ToInt32(ConfigurationManager.AppSettings["RememberMeCookieDays"] ?? "1");

                return rememberMeCookieDays;
            }
        }

        public static TimeSpan FormsTimeOut
        {
            get
            {
                TimeSpan timeout = FormsAuthentication.Timeout;

                return timeout;
            }
        }
    }
}
