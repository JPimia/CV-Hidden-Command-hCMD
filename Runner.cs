using NLog;
using NLog.Targets;
using NLog.Config;
using Newtonsoft.Json;

namespace hCMD
{
    internal class Runner
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public static void Main(string[] args)
        {
            SetupLogging();

            if (args.Length < 1)
            {
                logger.Trace("Usage: hCMD <command> <args>");
                return;
            }
            string jsonFilePath = "profiles.json";
            List<Profile> profiles = ProfileParser.Parse(jsonFilePath);

            string processName = args[0];
            string arguments = string.Join(" ", args.Skip(1));

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

            ProcessExecutor.GetInstance().Execute(processName, arguments);
        }

        private static void SetupLogging()
        {
            var configuration = new LoggingConfiguration();

            var fileTarget = new FileTarget("file")
            {
                FileName = "logs/console.log",
                CreateDirs = true,
            };

            var loggingRule = new LoggingRule("*", LogLevel.Trace, fileTarget);

            configuration.AddTarget(fileTarget);
            configuration.LoggingRules.Add(loggingRule);

            LogManager.Configuration = configuration;
            logger.Info("Logging setup complete");
        }
    }
}