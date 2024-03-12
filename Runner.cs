using NLog;
using System;
using hCMD.Tests;

namespace hCMD
{
    internal class Runner
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        public static void Main(string[] args)
        { 
            SetupLogging();
            Test.TestArguments_CommandLineArguments_ReturnString();
            // Program.Main(args);
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
            string arguments = args.Length > 1 ? string.Join(" ", args, 1, args.Length - 1) : "";

            switch (command.ToLower())
            {
                case "start":
                    if (string.IsNullOrWhiteSpace(arguments))
                    {
                        Console.WriteLine("Usage: hCMD start <process>");
                        return;
                    }
                    ProcessExecutor.Execute(command, arguments);
                    break;
                default:
                    Console.WriteLine("unknown command: <command>");
                    break;
            }
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