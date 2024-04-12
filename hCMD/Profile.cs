using Newtonsoft.Json;

namespace hCMD
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Profile
    {
        [JsonRequired]
        [JsonProperty("name")]
        public string Name;

        [JsonProperty("process_name")]
        public string ProcessToStart;

        [JsonProperty("arguments")]
        public string[] Arguments;

        public Profile(string name, string processToStart, string[] arguments)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Name cannot be whitespace only!");
            }

            Name = name.Trim();
            ProcessToStart = processToStart.Trim();
            Arguments = arguments;
        }

        public override bool Equals(object? obj)
        {
            if (obj == this)
            {
                return true;
            }
            else if (obj is Profile profile)
            {
                return profile.Name == Name &&
                       profile.ProcessToStart == ProcessToStart &&
                       profile.Arguments.SequenceEqual(Arguments);
            }

            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, ProcessToStart, Arguments);
        }
    }
}
