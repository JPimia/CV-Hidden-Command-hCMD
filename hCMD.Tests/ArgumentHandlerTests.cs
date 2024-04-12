namespace hCMD.Tests
{
    internal class ArgumentHandlerTests
    {
        [Test]
        public void OnShowUsageInstructions_Triggered()
        {
            var testArgumentHandler = new TestArgumentHandler
            {
                OnTestFailCallback = Assert.Fail,
                OnShowUsageInstructionsCallback = Assert.Pass
            };

            ArgumentHandler.OnArgumentsReceived(testArgumentHandler, []);
        }

        [Test]
        public void OnSetupEnvironmentPathCallback_Triggered()
        {
            var testArgumentHandler = new TestArgumentHandler
            {
                OnTestFailCallback = Assert.Fail,
                OnSetupEnvironmentPathCallback = Assert.Pass
            };

            ArgumentHandler.OnArgumentsReceived(testArgumentHandler, ["/updatePath"]);
        }

        [TestCase("echo", "test")]
        [TestCase("start", "notepad")]
        [TestCase("start", "/affinity 1 notepad")]
        [TestCase("echo", "test > file.txt")]
        public void OnRunCommandCallback_Triggered(string expectedCommand, string expectedArguments)
        {
            var testArgumentHandler = new TestArgumentHandler
            {
                OnTestFailCallback = Assert.Fail,
                OnRunCommandCallback = (command, args) =>
                {
                    Assert.That(command, Is.EqualTo(expectedCommand));
                    Assert.That(args, Is.EqualTo(expectedArguments));
                }
            };

            ArgumentHandler.OnArgumentsReceived(testArgumentHandler, [expectedCommand, expectedArguments]);
        }

        [TestCase()]
        [TestCase("echo", "test")]
        [TestCase("start", "notepad")]
        [TestCase("start", "/affinity 1 notepad")]
        [TestCase("echo", "test > file.txt")]
        public void OnAnyCallback_Triggered(params string[] arguments)
        {
            var testArgumentHandler = new TestArgumentHandler
            {
                OnTestFailCallback = Assert.Pass
            };

            ArgumentHandler.OnArgumentsReceived(testArgumentHandler, arguments);
            WaitForTestCompletion(100);
        }

        private static void WaitForTestCompletion(int timeout)
        {
            Thread.Sleep(timeout);

            Assert.Fail($"Test timed out after {timeout}ms: Something is wrong with test or implementation!");
        }
    }

    internal class TestArgumentHandler : IArgumentDelegate
    {
        internal Action<string, string>? OnAddProfileCallback;
        internal Action<string, string>? OnRunCommandCallback;
        internal Action? OnShowUsageInstructionsCallback;
        internal Action? OnSetupEnvironmentPathCallback;
        internal Action<string>? OnLoadProfileCallback;
        internal Action<string>? OnErrorCallback;

        internal required Action<string> OnTestFailCallback;

        public void OnAddProfile(string profileName, string profileSource)
        {
            if (OnAddProfileCallback == null)
            {
                OnTestFailCallback("Callback triggered: OnAddProfileCallback is null.");
                return;
            }

            OnAddProfileCallback(profileName, profileSource);
        }

        public void OnError(string errorMessage)
        {
            if (OnErrorCallback == null)
            {
                OnTestFailCallback("Callback triggered: OnErrorCallback is null.");
                return;
            }

            OnErrorCallback(errorMessage);
        }

        public void OnLoadProfile(string profileName)
        {
            if (OnLoadProfileCallback == null)
            {
                OnTestFailCallback("Callback triggered: OnLoadProfileCallback is null.");
                return;
            }

            OnLoadProfileCallback(profileName);
        }

        public void OnRunCommand(string command, string arguments)
        {
            if (OnRunCommandCallback == null)
            {
                OnTestFailCallback("Callback triggered: OnRunCommandCallback is null.");
                return;
            }

            OnRunCommandCallback(command, arguments);
        }

        public void OnSetupEnvironmentPath()
        {
            if (OnSetupEnvironmentPathCallback == null)
            {
                OnTestFailCallback("Callback triggered: OnSetupEnvironmentPathCallback is null.");
                return;
            }

            OnSetupEnvironmentPathCallback();
        }

        public void OnShowUsageInstructions()
        {
            if (OnShowUsageInstructionsCallback == null)
            {
                OnTestFailCallback("Callback triggered: OnShowUsageInstructionsCallback is null.");
                return;
            }

            OnShowUsageInstructionsCallback();
        }
    }
}
