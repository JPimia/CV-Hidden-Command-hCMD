using System;
using System.Diagnostics;
using NLog;

namespace hCMD
{
    internal class ProcessExecutor
    {
        private static ProcessExecutor _instance;
        private static readonly object _lock = new object();
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        private ProcessExecutor() { }

        public static ProcessExecutor GetInstance()
        {
            lock (_lock)
            {
                if (_instance == null)
                {
                    _instance = new ProcessExecutor();
                }
                return _instance;
            }
        }
    }
}
