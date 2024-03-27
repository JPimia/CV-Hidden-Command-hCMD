using Newtonsoft.Json;
using NLog;

namespace hCMD
{
    public static class ProfileParser
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public static List<Profile>? Parse(string filePath) => ParseJson(File.ReadAllText(filePath));

        public static List<Profile>? ParseJson(string jsonData)
        {
            List<Profile>? profiles = null;

            try
            {
                profiles = JsonConvert.DeserializeObject<List<Profile>>(jsonData);
            }
            catch (JsonSerializationException exception)
            {
                logger.Warn(exception);
            }

            return profiles;
        }

        public static Profile? GetProfile(string filePath, string profileName) => FindProfileByName(Parse(filePath) ?? [], profileName);

        public static Profile? FindProfileByName(List<Profile> profiles, string profileName) => profiles.Find(profile => profile.Name == profileName);
    }
}
