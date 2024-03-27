namespace hCMD
{
    public interface IArgumentDelegate
    {
        public void OnRunCommand(string command, string arguments);

        public void OnAddProfile(string profileName, string profileSource);
        public void OnLoadProfile(string profileName);
        public void OnSetupEnvironmentPath();

        public void OnError(string errorMessage);

        public void OnShowUsageInstructions();
    }
}
