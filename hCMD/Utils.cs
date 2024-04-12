using NLog;

namespace hCMD
{
    public static class Utils
    {
        private static readonly string PATH_VARIABLE = "PATH";
        private static readonly string PATH_EXT_VARIABLE = "PATHEXT";

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

        internal static bool ExecutableExistsInPathVariable(string executable)
        {
            var environmentPath = Environment.GetEnvironmentVariable(PATH_VARIABLE);
            var pathExtension = Environment.GetEnvironmentVariable(PATH_EXT_VARIABLE);

            if (environmentPath == null)
            {
                logger.Fatal($"Environment variable named '{PATH_VARIABLE}' was not found!");

                return false;
            }
            else if (pathExtension == null) 
            {
                logger.Fatal($"Environment variable named '{PATH_EXT_VARIABLE}' was not found!");

                return false;
            }

            return ExecutableExistsInPathString(environmentPath, pathExtension, executable);
        }

        public static bool IncludedInPathString(string environmentPath, string directory)
        {
            var pathToSearch = directory.NormalizePath();

            foreach (var path in environmentPath.Split(';'))
            {
                if (string.IsNullOrWhiteSpace(path)) continue;

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

        public static bool IsValidFileName(string path)
        {
            var filename = Path.GetFileName(path).Trim();

            return filename.Length > 0
                   && !filename.EndsWith('.')
                   && filename.IndexOfAny(Path.GetInvalidFileNameChars()) < 0;
        }

        public static bool ExecutableExistsInPathString(string environmentPath, string pathExtension, string executable)
        {
            if (!Path.HasExtension(executable))
            {
                foreach (var extension in pathExtension.Split(';'))
                {
                    if (ExecutableExistsInPathString(environmentPath, Path.ChangeExtension(executable, extension)))
                    {
                        return true;
                    }
                }

                return false;
            }

            return ExecutableExistsInPathString(environmentPath, executable);
        }

        public static bool ExecutableExistsInPathString(string environmentPath, string executable)
        {
            var executablePath = Environment.ExpandEnvironmentVariables(executable);

            if (!File.Exists(executablePath))
            {
                var executableFilename = Path.GetFileName(executablePath);

                foreach (var path in environmentPath.Split(';'))
                {
                    if (File.Exists(Path.Combine(path.Trim(), executableFilename)))
                    {
                        return true;
                    }
                }

                return false;
            }

            return true;
        }

        private static string NormalizePath(this string path)
        {
            return Path.TrimEndingDirectorySeparator(Path.GetFullPath(path));
        }
    }
}
