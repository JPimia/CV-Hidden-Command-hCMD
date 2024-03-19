using Newtonsoft.Json;

namespace hCMD
{
    public static class ProfileParser
    {
        public static List<Profile>? Parse(string filePath) => ParseJson(File.ReadAllText(filePath));

        public static List<Profile>? ParseJson(string jsonData) => JsonConvert.DeserializeObject<List<Profile>>(jsonData);

        public static Profile? GetProfile(string filePath, string profileName) => FindProfileByName(Parse(filePath) ?? [], profileName);

        public static Profile? FindProfileByName(List<Profile> profiles, string profileName) => profiles.Find(profile => profile.Name == profileName);
    }
}
