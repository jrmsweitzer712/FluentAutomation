using System;
using System.Configuration;
using System.IO;

namespace FluentFramework.Core
{
    public class Settings
    {
        public Settings()
        {

        }

        public string SeleniumDirectory => ConfigurationManager.AppSettings["seleniumDirectory"] ?? "C:/Selenium";
        public string DriverDirectory => $"{SeleniumDirectory}/Drivers";
        public string ServerDirectory => $"{SeleniumDirectory}/SeleniumServer";

        public string ExternalResources
        {
            get
            {
                return ConfigurationManager.AppSettings["externalResources"];
            }
        }

        private TimeSpan? _commandTimeout = null;
        public TimeSpan CommandTimeout
        {
            get
            {
                if (_commandTimeout == null)
                    _commandTimeout = new TimeSpan(seconds: int.Parse(ConfigurationManager.AppSettings["commandTimeout"] ?? "30"), hours: 0, minutes: 0);
                return _commandTimeout.Value;
            }
            set
            {
                _commandTimeout = value;
            }
        }

        private string _browser;
        public string Browser
        {
            get
            {
                if (string.IsNullOrEmpty(_browser))
                    _browser = ConfigurationManager.AppSettings["browser"] ?? "Chrome";
                return _browser;
            }
            set
            {
                _browser = value;
            }
        }

        public bool Mobile = false;
    }
}
