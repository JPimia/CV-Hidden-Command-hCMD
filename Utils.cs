using System;

namespace hCMD
{
    // TODO: Implement methods to check environment PATH variable contents and manipulate it
    internal class Utils
    {
        public static bool IncludedInPathVariable()
        {
            var currentDirectory = Directory.GetCurrentDirectory();

            // TODO: Implement checking for environment PATH variable (if current directory is included)
            // TODO: Get environment variable PATH (user and system scoped) loop through those paths to see if current dir is there

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
