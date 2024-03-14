using Newtonsoft.Json;

namespace hCMD
{
    internal class ProfileParser
    {
        public static List<Profile> Parse(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"File not found: {filePath}");
            }
            return JsonConvert.DeserializeObject<List<Profile>>(File.ReadAllText(filePath));
        }
    }
}
