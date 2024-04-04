using Newtonsoft.Json;
using NLog;

namespace hCMD
{
    internal class ProfileEditor
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        public static void AddProfile(string fileName, string source)
        {
            string extension = Path.GetExtension(source);
            string jsonFilePath = "profiles.json";
            string profileContent = File.ReadAllText(source);
            string[] lines = profileContent.Split('\n');
            string[] trimmedLines = lines.Select(line => line.Trim()).ToArray();

            List<Profile> profiles = ProfileParser.Parse(jsonFilePath) ?? new List<Profile>();

            try
            {
                switch (extension)
                {
                    case ".bat":
                        profiles.Add(new Profile { Name = fileName, ProcessToStart = "cmd.exe", Arguments = trimmedLines });
                        break;
                    case ".ps1":
                        profiles.Add(new Profile { Name = fileName, ProcessToStart = "ps", Arguments = trimmedLines });
                        break;
                    case ".cmd":
                        profiles.Add(new Profile { Name = fileName, ProcessToStart = "cmd.exe", Arguments = trimmedLines });
                        break;
                    case ".vbs":
                        profiles.Add(new Profile { Name = fileName, ProcessToStart = "wscript", Arguments = trimmedLines });
                        break;
                    default:
                        logger.Error("Invalid file extension");
                        break;
                }
                
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
