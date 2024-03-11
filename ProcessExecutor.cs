using System;

namespace hCMD
{
    internal class ProcessExecutor
    {
        public static void Execute(string command, string arguments)
        {
            if (string.IsNullOrWhiteSpace(command) || string.IsNullOrWhiteSpace(arguments))
            {
                Console.WriteLine("Error");
            }
            else
            {
                Console.WriteLine("Moro");
            }
        }
    }
}
