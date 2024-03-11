using NLog;
using System;

namespace hCMD
{
    internal class Runner
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        public static void Main(string[] args)
        { 
            SetupLogging();
            Console.WriteLine("You can't see this output due to app being in windowed mode.");
            logger.Info("This is test message");
        }

        private static void SetupLogging()
        {
            // TODO: Include NLog Nuget package to project references
            // TODO: Setup NLog here

            NLog.LogManager.LoadConfiguration("NLog.config");
        }
    }
}