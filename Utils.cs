using NLog;
using System;

namespace hCMD
{
    // TODO: Implement methods to check environment PATH variable contents and manipulate it
    internal class Utils
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        public static bool IncludedInPathVariable()
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            var paths = Environment.GetEnvironmentVariable("PATH");
            if (string.IsNullOrEmpty(paths))
            {
                return false;
            }
            var splitPaths = paths.Split(';');
            foreach (var path in splitPaths)
            {
                if (path == currentDirectory)
                {
                    return true;
                }
            }
            return false;
        }

        public static void UpdatePathVariable()
        {
            if (IncludedInPathVariable()) return;

            var scope = EnvironmentVariableTarget.User;
            var userPath = Environment.GetEnvironmentVariable("PATH", scope);
            var currentDirectory = Directory.GetCurrentDirectory();

            if (userPath != null)
            {
                Environment.SetEnvironmentVariable("PATH", $"{userPath};{currentDirectory}", scope);
            }
        }
    }
}
