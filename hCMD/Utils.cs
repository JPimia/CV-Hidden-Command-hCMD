using NLog;

namespace hCMD
{
    public static class Utils
    {
        private static readonly string PATH_VARIABLE = "PATH";
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        internal static bool IncludedInPathVariable()
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            var pathData = Environment.GetEnvironmentVariable(PATH_VARIABLE);

            if (pathData == null)
            {
                logger.Fatal($"Environment variable named '{PATH_VARIABLE}' was not found!");
                return false;
            }

            return IncludedInPathString(pathData, currentDirectory);
        }

        internal static void UpdatePathVariable()
        {
            if (IncludedInPathVariable()) return;

            var scope = EnvironmentVariableTarget.User;
            var userPath = Environment.GetEnvironmentVariable(PATH_VARIABLE, scope);
            var currentDirectory = Directory.GetCurrentDirectory();

            if (userPath == null)
            {
                logger.Fatal($"Environment variable named '{PATH_VARIABLE}' was not found!");
                return;
            }

            Environment.SetEnvironmentVariable(PATH_VARIABLE, AppendToPathString(userPath, currentDirectory), scope);
        }

        public static bool IncludedInPathString(string environmentPath, string directory)
        {
            var pathToSearch = directory.NormalizePath();

            foreach (var path in environmentPath.Split(';'))
            {
                if (pathToSearch.Equals(path.NormalizePath(), StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }

        public static string AppendToPathString(string environmentPath, string directory)
        {
            var pathToAppend = directory.NormalizePath();

            if (string.IsNullOrEmpty(directory))
            {
                throw new ArgumentException("Given directory argument was empty!");
            }
            else if (pathToAppend.Contains(';'))
            {
                throw new ArgumentException("Given directory is not singlular and/or valid path!");
            }

            return $"{environmentPath};{pathToAppend}";
        }

        private static string NormalizePath(this string path)
        {
            return Path.TrimEndingDirectorySeparator(Path.GetFullPath(path));
        }
    }
}
