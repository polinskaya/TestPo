using System;
using System.Configuration;
using NUnit.Framework;

namespace SeleniumWebdriver.Services
{
    public static class TestDataReader
    {
        private static Configuration ConfigFile
        {
            get
            {
                var variable = TestContext.Parameters.Get("environment");
                var file = string.IsNullOrEmpty(variable) ? "dev" : variable;
                var separateIndex = AppDomain.CurrentDomain.BaseDirectory.IndexOf("bin", StringComparison.Ordinal);
                var customConfigFileMap = new ExeConfigurationFileMap
                {
                    ExeConfigFilename = AppDomain.CurrentDomain.BaseDirectory.Substring(0, separateIndex) +
                                        @"Configuration\" + file + ".config"
                };
                return ConfigurationManager.OpenMappedExeConfiguration(customConfigFileMap, ConfigurationUserLevel.None);
            }
        }

        public static string GetTestData(string key)
        {
            return ConfigFile.AppSettings.Settings[key]?.Value;
        }
    }
}
