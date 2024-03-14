using System.Diagnostics;
using NLog;

namespace hCMD
{
    internal class ProcessExecutor
    {
        private static ProcessExecutor? _instance;

        private static readonly object _lock = new();
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        private ProcessExecutor() {}

        public static ProcessExecutor GetInstance()
        {
            lock (_lock)
            {
                if (_instance == null)
                {
                    _instance = new ProcessExecutor();
                    logger.Trace("ProcessExecutor instance created");
                }

                return _instance;
            }
        }

        public void Execute(string processName, string arguments)
        {
            try
            {
                var process = new Process();

                process.StartInfo = new ProcessStartInfo {
                    FileName = "cmd.exe",
                    Arguments = $"/c {processName} {arguments}",
                    UseShellExecute = false
                };

                process.Start();
                logger.Info($"Process '{processName}' executed with arguments '{arguments}'");
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error executing process");
            }
        }
        public void LoadProfile(Profile profile)
        {
            if (profile.ProcessToStart == null || profile.Arguments == null)
            {
                logger.Error("ProcessToStart or Arguments are null in the profile");
                return;
            }
            Execute(profile.ProcessToStart, profile.Arguments);
        }
    }
}
