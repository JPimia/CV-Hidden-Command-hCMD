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
            //var scope = EnvironmentVariableTarget.User;
            var currentDirectory = Directory.GetCurrentDirectory();
            //var paths = ...;

            // TODO: Include current directory in environment PATH variable
            // TODO: Get environment variable "PATH" for User and append current path to the end (if it's not there)
            // TODO: Replace existing environment variable "PATH" (user scoped) with new PATH
        }
    }
}
