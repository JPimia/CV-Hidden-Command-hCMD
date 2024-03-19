using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            int index = 0;
            foreach (Profile profile in profiles)
            {
                Assert.That(profile.Name.ToLower() == profile.Name.ToLower());
                Assert.That(profile.ProcessToStart.ToLower() == profile.ProcessToStart.ToLower());
                Assert.That(profile.Arguments.ToLower() == profile.Arguments.ToLower());
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
