using System;

namespace hCMD
{
    internal class Runner
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("You can't see this output due to app being in windowed mode.");

        }

        private static void SetupLogging()
        {
            // TODO: Include NLog Nuget package to project references
            // TODO: Setup NLog here
        }
    }
}