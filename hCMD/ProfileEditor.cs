using Newtonsoft.Json;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hCMD
{
    internal class ProfileEditor
    {
        public static void AddProfile(string profileFile)
        {
            Logger logger = LogManager.GetCurrentClassLogger();
            string profileContent = File.ReadAllText(profileFile);
            string[] lines = profileContent.Split('\n');
            string jsonFilePath = "profiles.json";
            List<Profile> profiles = ProfileParser.Parse(jsonFilePath) ?? new List<Profile>();

            string Name = "";
            string ProcessToStart = "";
            string Arguments = "";

            foreach (string line in lines)
            {
                if (line.Contains("Name"))
                {
                    Name = line.Split('=')[1].Trim();
                }
                else if (line.Contains("ProcessToStart"))
                {
                    ProcessToStart = line.Split('=')[1].Trim();
                }
                else if (line.Contains("Arguments"))
                {
                    Arguments = line.Split('=')[1].Trim();
                }
            }

            Profile newProfile = new Profile { Name = Name, ProcessToStart = ProcessToStart, Arguments = Arguments };

            profiles.Add(newProfile);

            string json = JsonConvert.SerializeObject(profiles, Formatting.Indented);
            File.WriteAllText(jsonFilePath, json);
            logger.Info($"Profile {Name} added to {jsonFilePath}");
        }
    }
}
