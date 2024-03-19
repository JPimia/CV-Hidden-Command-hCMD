using Newtonsoft.Json;

namespace hCMD
{
    public class ProfileParser
    {
        public static List<Profile> Parse(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"File not found: {filePath}");
            }
            if (new FileInfo(filePath).Length == 0)
            {
                throw new InvalidDataException($"File is empty: {filePath}");
            }
            return JsonConvert.DeserializeObject<List<Profile>>(File.ReadAllText(filePath))!;
        }
    }
}
