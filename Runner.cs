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
            // Console.WriteLine("You can't see this output due to app being in windowed mode.");
            // logger.Info("This is test message");
            // logger.Debug("This is test message");
            // Console.Read();
            if (args.Length < 1)
            {
                Console.WriteLine("Usage: hCMD <command> <args>");
                return;
            }
            string command = args[0];
            string arguments = string.Join(" ", args, 1, args.Length - 1);
        }

        private static void SetupLogging()
        {
            // TODO: Include NLog Nuget package to project references
            // TODO: Setup NLog here
            Console.WriteLine("Setting up logging");
            NLog.LogManager.LoadConfiguration("NLog.config");
        }
    }
}