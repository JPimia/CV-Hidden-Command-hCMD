using Newtonsoft.Json;
using NLog;

namespace hCMD
{
    internal class ProfileEditor
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        public static void AddProfile(string fileName, string source)
        {
            try
            {
                string profileContent = File.ReadAllText(source);
                string[] lines = profileContent.Split('\n');
                string destinationFilePath = $"{fileName}.bat";
                File.Copy(source, destinationFilePath);
                

                string jsonFilePath = "profiles.json";
                List<Profile> profiles = ProfileParser.Parse(jsonFilePath) ?? new List<Profile>();

                string[] arguments = lines.Skip(2).ToArray();

                Profile newProfile = new Profile { Name = lines[0], ProcessToStart = lines[1], Arguments = arguments };

                profiles.Add(newProfile);

                string json = JsonConvert.SerializeObject(profiles, Formatting.Indented);
                File.WriteAllText(jsonFilePath, json);
                logger.Info($"Profile {fileName} added to {jsonFilePath}");
            }
            catch (Exception e)
            {
                logger.Error(e.Message, "error while adding profile");
                throw;
            }
        }
    }
}
