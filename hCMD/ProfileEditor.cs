using Newtonsoft.Json;
using NLog;

namespace hCMD
{
    internal class ProfileEditor
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        public static void AddProfile(string profileFile)
        {
            try
            {
                string profileContent = File.ReadAllText(profileFile);
                string[] lines = profileContent.Split('\n');

                string Name = "";
                string ProcessToStart = "";
                string Arguments = "";

                foreach (string line in lines)
                {
                    if (line.Contains("Name"))
                    {
                        Name = GetValueFromLine(line);
                    }
                    else if (line.Contains("ProcessToStart"))
                    {
                        ProcessToStart = GetValueFromLine(line);
                    }
                    else if (line.Contains("Arguments"))
                    {
                        Arguments = GetValueFromLine(line);
                    }
                }

                string jsonFilePath = "profiles.json";
                List<Profile> profiles = ProfileParser.Parse(jsonFilePath) ?? new List<Profile>();

                Profile newProfile = new Profile { Name = Name, ProcessToStart = ProcessToStart, Arguments = Arguments };

                profiles.Add(newProfile);

                string json = JsonConvert.SerializeObject(profiles, Formatting.Indented);
                File.WriteAllText(jsonFilePath, json);
                logger.Info($"Profile {Name} added to {jsonFilePath}");
            }
            catch (Exception e)
            {
                logger.Error(e.Message, "error while adding profile");
                throw;
            }
        }
        public static string GetValueFromLine(string line)
        {
            return line.Split("=")[1].Trim();
        }
    }
}
