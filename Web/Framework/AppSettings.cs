namespace forCrowd.Website.Web.Framework
{
    using System;
    using System.Configuration;

    public enum EnvironmentType
    {
        Local,
        Test,
        Live
    }

    public static class AppSettings
    {
        /// <summary>
        /// Email service etc. settings vary on based on environment type
        /// Local | Test | Live
        /// </summary>
        public static EnvironmentType EnvironmentType
        {
            get { return (EnvironmentType)Enum.Parse(typeof(EnvironmentType), ConfigurationManager.AppSettings["Environment"]); }
        }

        /// <summary>
        /// Contact emails will be send from this address
        /// </summary>
        public static string ContactEmailAddress
        {
            get { return ConfigurationManager.AppSettings["ContactEmailAddress"]; }
        }

        /// <summary>
        /// Notification emails will be send to this address
        /// </summary>
        public static string NotificationEmailAddress
        {
            get { return ConfigurationManager.AppSettings["NotificationEmailAddress"]; }
        }
    }
}