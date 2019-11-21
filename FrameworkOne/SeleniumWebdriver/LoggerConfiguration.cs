using System;
using log4net;
using log4net.Appender;
using log4net.Core;
using log4net.Layout;
using log4net.Repository.Hierarchy;

namespace SeleniumWebdriver
{
    public abstract class LoggerConfiguration
    {
        public static ILog Log { get; } = LogManager.GetLogger("root");

        public static void InitLogger()
        {
            var hierarchy = (Hierarchy)LogManager.GetRepository();

            var patternLayout =
                new PatternLayout
                {
                    ConversionPattern = "%date [%thread] %-5level %logger - %message%newline"
                };
            patternLayout.ActivateOptions();

            var roller = new RollingFileAppender
            {
                AppendToFile = true,
                File = $"{@"Logs\Log"}{DateTime.Now.ToString("yy-MM-dd")}.txt",
                Layout = patternLayout,
                MaxSizeRollBackups = 5,
                MaximumFileSize = "1MB",
                RollingStyle = RollingFileAppender.RollingMode.Size,
                StaticLogFileName = true
            };
            roller.ActivateOptions();
            hierarchy.Root.AddAppender(roller);

            var memory = new MemoryAppender();
            memory.ActivateOptions();
            hierarchy.Root.AddAppender(memory);

            var consoleAppender = new ConsoleAppender();
            consoleAppender.ActivateOptions();
            hierarchy.Root.AddAppender(consoleAppender);

            hierarchy.Root.Level = Level.All;
            hierarchy.Configured = true;
        }
    }
}
