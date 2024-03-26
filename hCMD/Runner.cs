using NLog;
using NLog.Targets;
using NLog.Config;

namespace hCMD
{
    public class Runner
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

            string action = args[1];
            var executor = ProcessExecutor.GetInstance();

            switch (action)
            {
                case "/updatePath":
                    SetupPathVariable();
                    break;
                case "/addProfile":
                    ProfileEditor.AddProfile();
                    break;
                case "/profile":
                    if (args.Length < 2)
                    {
                        logger.Warn("Missing profile identifier!");
                        return;
                    }

                    var profileName = args[1];
                    var profile = ProfileParser.GetProfile("profiles.json", profileName);


                    if (profile == null)
                    {
                        logger.Warn($"Profile {profileName} was not found!");
                        return;
                    }

                    executor.LoadProfile(profile);
                    break;
                default:
                    executor.Execute(action, string.Join(" ", args.Skip(1)));
                    break;
            }
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

        private static void SetupPathVariable()
        {
            if (Utils.IncludedInPathVariable())
            {
                logger.Info("Environment PATH variable already contains current directory.");
                return;
            }

            logger.Info("Environment PATH variable updated.");
            Utils.UpdatePathVariable();
        }
    }
}