using System.Reflection;

namespace hCMD.Tests
{
    public class ProfileJsonTests
    {
        string jsonFilePath = "profiles.json";
        [Test]
        public void ProfilePropertiesIsNotEmpty_returnsTrue()
        {
            List<Profile> profiles = ProfileParser.Parse(jsonFilePath);

            foreach (Profile profile in profiles)
            {
                Assert.IsTrue(!string.IsNullOrEmpty(profile.Name));
                Assert.IsTrue(!string.IsNullOrEmpty(profile.ProcessToStart));
                Assert.IsTrue(!string.IsNullOrEmpty(profile.Arguments));
            }
        }

        [Test]
        public void ProfilePropertiesIgnoreCase()
        {
            List<Profile> profiles = ProfileParser.Parse(jsonFilePath);
            foreach (Profile profile in profiles)
            {
                PropertyInfo[] properties = typeof(Profile).GetProperties();
                foreach (var property in properties)
                {
                    object value = property.GetValue(profile)!;
                    if (value is string stringValue)
                    {
                        Assert.That(stringValue.ToLower(), Is.EqualTo(stringValue!.ToLower()));
                    }
                }
            }
        }

        [Test]
        public void ProfilePropertiesAreCorrectType()
        {
            List<Profile> profiles = ProfileParser.Parse(jsonFilePath);

            foreach (Profile profile in profiles)
            {
                Assert.That(profile.Name, Is.TypeOf<string>());
                Assert.That(profile.ProcessToStart, Is.TypeOf<string>());
                Assert.That(profile.Arguments, Is.TypeOf<string>());
            }
        }

        [Test]
        public void ProfileHasAllDefaultProperties()
        {
            List<Profile> profiles = ProfileParser.Parse(jsonFilePath);

            foreach (Profile profile in profiles)
            {
                Assert.That(profile.Name, Is.Not.Null);
                Assert.That(profile.ProcessToStart, Is.Not.Null);
                Assert.That(profile.Arguments, Is.Not.Null);
            }
        }
    }
}
