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
        public static void AddProfile()
        {
            Logger logger = LogManager.GetCurrentClassLogger();
            string jsonFilePath = "profiles.json";
            List<Profile> profiles = ProfileParser.Parse(jsonFilePath) ?? new List<Profile>();

            Console.WriteLine("Enter profile name:");
            string profileName = Console.ReadLine() ?? "";
            Console.WriteLine("Enter process to start:");
            string processToStart = Console.ReadLine() ?? "";
            Console.WriteLine("Enter arguments:");
            string arguments = Console.ReadLine() ?? "";

            Profile newProfile = new Profile { Name = profileName, ProcessToStart = processToStart, Arguments = arguments };

            profiles.Add(newProfile);

            string json = JsonConvert.SerializeObject(profiles, Formatting.Indented);
            File.WriteAllText(jsonFilePath, json);
            logger.Info($"Profile {profileName} added to {jsonFilePath}");
        }
    }
}
