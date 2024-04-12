using Newtonsoft.Json;

namespace hCMD.Tests
{
    public class ProfileParserTests
    {
        [TestCase("[{}]")]
        [TestCase("[{\"name\": null}]")]
        [TestCase("[{\"name\": \"\"}]")]
        [TestCase("[{\"name\": \" \"}]")]
        [TestCase("[{\"process_name\": \"cmd\"}]")]
        public void ParseIncorrectlyFormattedProfileFromString_ReturnsNull(string jsonData)
        {
            Assert.That(ProfileManager.ParseJson(jsonData), Is.Null);
        }

        [Test]
        public void ParseEmptyProfileFromFile_IsEqual()
        {
            var jsonFile = GetResourceFile("empty_profile.json");
            var profiles = ProfileManager.Parse(jsonFile);
            
            Assert.That(profiles, Has.Count.Zero);
            Assert.That(profiles, Is.EqualTo(new List<Profile>()));
        }

        [Test]
        public void ParseSingleProfileFromFile_IsEqual()
        {
            var jsonFile = GetResourceFile("single_profile.json");
            var profiles = ProfileManager.Parse(jsonFile);

            Assert.That(profiles, Has.Count.EqualTo(1));

            List<Profile> expectedProfiles = [
                new("testProfile", "cmd", [])
            ];

            Assert.That(profiles, Is.EqualTo(expectedProfiles));
        }

        [Test]
        public void ParseMultipleProfilesFromFile_AllEqual()
        {
            var jsonFile = GetResourceFile("multi_profile.json");
            var profiles = ProfileManager.Parse(jsonFile);

            Assert.That(profiles, Has.Count.EqualTo(5));

            for (var i = 0; i < profiles.Count; i++)
            {
                var arguments = Enumerable.Range(1, i + 1).Select((value, _) => value.ToString());
                var expectedProfile = new Profile($"test{i}", "cmd", arguments.ToArray());

                Assert.That(profiles[i], Is.EqualTo(expectedProfile), "Profiles are not equal!");
            }
        }

        [Test]
        public void ParseEmptyProfileFromModel_IsEqual()
        {
            List<Profile> expectedProfiles = [];

            var jsonData = JsonConvert.SerializeObject(expectedProfiles);
            var profiles = ProfileManager.ParseJson(jsonData);

            Assert.That(profiles, Has.Count.Zero);
            Assert.That(profiles, Is.EquivalentTo(expectedProfiles));
        }

        [Test]
        public void ParseSingleProfileFromModel_IsEqual()
        {
            var expectedProfile = new Profile("test", "cmd", ["test"]);
            List<Profile> expectedProfiles = [expectedProfile];

            var jsonData = JsonConvert.SerializeObject(expectedProfiles);
            var profiles = ProfileManager.ParseJson(jsonData);

            Assert.That(profiles, Is.EquivalentTo(expectedProfiles));
        }

        [Test]
        public void ParseMultipleProfilesFromModel_AllEqual()
        {
            var expectedProfiles = new List<Profile>
            {
                new("CMD", "cmd", ["1"]),
                new("PS", "ps", ["1", "2"]),
                new("VBS", "wscript", ["1", "2", "3"])
            };

            var jsonData = JsonConvert.SerializeObject(expectedProfiles);
            var profiles = ProfileManager.ParseJson(jsonData);

            Assert.That(profiles, Is.EquivalentTo(expectedProfiles));
        }

        [Test]
        public void FindProfileByName_ReturnsCorrectProfile()
        {
            var cmdProfile = new Profile("cmdTest", "cmd", ["echo", "test"]);
            var psProfile = new Profile("psTest", "ps", ["Start-Process", "notepad"]);
            var vbsProfile = new Profile("vbsTest", "wscript", ["Print", "\"test\""]);
            List<Profile> profiles = [cmdProfile, psProfile, vbsProfile];

            Assert.That(ProfileManager.FindProfileByName(profiles, "cmdTest"), Is.SameAs(cmdProfile));
            Assert.That(ProfileManager.FindProfileByName(profiles, "psTest"), Is.SameAs(psProfile));
            Assert.That(ProfileManager.FindProfileByName(profiles, "vbsTest"), Is.SameAs(vbsProfile));
        }

        [TestCase("")]
        [TestCase("testProfile")]
        [TestCase("CmdTest")]
        [TestCase("PSTEST")]
        [TestCase("vbstest")]
        public void FindProfileByName_ReturnsNull(string profileName)
        {
            var cmdProfile = new Profile("cmdTest", "cmd", ["echo", "test"]);
            var psProfile = new Profile("psTest", "ps", ["Start-Process", "notepad"]);
            var vbsProfile = new Profile("vbsTest", "wscript", ["Print", "\"test\""]);
            List<Profile> profiles = [cmdProfile, psProfile,vbsProfile];

            Assert.That(ProfileManager.FindProfileByName(profiles, profileName), Is.Null);
        }

        private static string GetResourceFile(string fileName)
        {
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", fileName);
        }
    }
}
