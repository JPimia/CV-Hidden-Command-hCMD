using NLog;

namespace hCMD
{
    // TODO: Implement methods to check environment PATH variable contents and manipulate it
    public static class Utils
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        internal static bool IncludedInPathVariable()
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            var paths = Environment.GetEnvironmentVariable("PATH");

            return IncludedInPathString(paths ?? "", currentDirectory);
        }

        internal static void UpdatePathVariable()
        {
            if (IncludedInPathVariable()) return;

            var scope = EnvironmentVariableTarget.User;
            var userPath = Environment.GetEnvironmentVariable("PATH", scope);
            var currentDirectory = Directory.GetCurrentDirectory();

            if (userPath != null)
            {
                Environment.SetEnvironmentVariable("PATH", AppendToPathString(userPath, currentDirectory), scope);
            }
        }

        public static bool IncludedInPathString(string environmentPath, string directory)
        {
            foreach (var path in environmentPath.Split(';'))
            {
                if (directory.Equals(path, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }

        public static string AppendToPathString(string environmentPath, string directory)
        {
            if (string.IsNullOrEmpty(directory))
            {
                throw new ArgumentException("directory argument was empty!");
            }
            else if (directory.Contains(';'))
            {
                throw new ArgumentException("directory argument contains multiple paths!");
            }

            return $"{environmentPath};{directory}";
        }
    }
}
