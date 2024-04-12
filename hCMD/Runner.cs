using NLog;
using NLog.Targets;
using NLog.Config;

namespace hCMD
{
    public class Runner : IArgumentDelegate
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public static void Main(string[] args)
        {
            SetupLogging();

            ArgumentHandler.OnArgumentsReceived(new Runner(), args);
        }

        public void OnAddProfile(string profileName, string profileSource)
        {
            //throw new NotImplementedException();
        }

        public void OnError(string errorMessage)
        {
            logger.Error(errorMessage);
        }

        public void OnLoadProfile(string profileName)
        {
            var executor = ProcessExecutor.GetInstance();
            var profile = ProfileManager.GetProfile("profiles.json", profileName);

            if (profile == null)
            {
                logger.Warn($"Profile {profileName} was not found!");
                return;
            }

            executor.LoadProfile(profile);
        }

        public void OnRunCommand(string command, string arguments)
        {
            var executor = ProcessExecutor.GetInstance();

            executor.Execute(command, arguments);
        }

        public void OnSetupEnvironmentPath()
        {
            if (Utils.IncludedInPathVariable())
            {
                logger.Info("Environment PATH variable already contains current directory.");
                return;
            }

            logger.Info("Updating environment PATH variable.");
            Utils.UpdatePathVariable();
        }

        public void OnShowUsageInstructions()
        {
            logger.Trace("Usage: hCMD <command> <args>");
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