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
        public void CheckForProfileName_returnsTrue()
        {
            List<Profile> profiles = ProfileParser.Parse(jsonFilePath);

            foreach (Profile profile in profiles)
            {
                Assert.IsTrue(!string.IsNullOrEmpty(profile.Name));
            }
        }
    }
}
