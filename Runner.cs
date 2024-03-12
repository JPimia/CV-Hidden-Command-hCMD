using NLog;
using NLog.Targets;
using NLog.Config;
using System.Diagnostics;

namespace hCMD
{
    internal class Runner
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        private static Process process = new Process();
        public static void Main(string[] args)
        {
            SetupLogging();

            //Test.TestArguments_CommandLineArguments_ReturnString();
            // Program.Main(args);
            // Console.WriteLine("You can't see this output due to app being in windowed mode.");
            // logger.Info("This is test message");
            // logger.Debug("This is test message");
            // Console.Read();

            string processName = args[0];
            string arguments = args.Length > 1 ? string.Join(" ", args, 1, args.Length - 1) : "";

            if (args.Length < 1)
            {
                logger.Trace("Usage: hCMD <command> <args>");
                return;
            }
            //string command = args[0];
            //string arguments = args.Length > 1 ? string.Join(" ", args, 1, args.Length - 1) : "";

            //switch (command.ToLower())
            //{
            //    case "start":
            //        if (string.IsNullOrWhiteSpace(arguments))
            //        {
            //            logger.Trace("Usage: hCMD start <process>");
            //            return;
            //        }
            //        ProcessExecutor.Execute(command, arguments);
            //        break;
            //    default:
            //        logger.Trace("unknown command: <command>");
            //        break;   
            //}
            ProcessExecutor executor = ProcessExecutor.GetInstance();
            executor.Execute(processName, arguments);
        }

        private static void SetupLogging()
        {
            var configuration = new LoggingConfiguration();

            var fileTarget = new FileTarget("file")
            {
                CreateDirs = true,
                FileName = "logs/console.log"
            };

            var loggingRule = new LoggingRule("*", LogLevel.Trace, fileTarget);

            configuration.AddTarget(fileTarget);
            configuration.LoggingRules.Add(loggingRule);

            LogManager.Configuration = configuration;
            logger.Info("Logging setup complete");
        }
    }
}