using System.Diagnostics;
using System.Xml.Linq;

namespace hCMD.Tests
{
    internal class ProfileTests
    {
        private static readonly string DEFAULT_NAME = "testName";
        private static readonly string DEFAULT_PROCESS = "testProcess";

        [TestCase("")]
        [TestCase(" ")]
        [TestCase("\t")]
        public void Profile_NameThrowsArgumentException(string name)
        {
            Assert.Throws(
                typeof(ArgumentException),
                () => new Profile(name, DEFAULT_PROCESS, [])
            );
        }

        [TestCase("a")]
        [TestCase("X")]
        [TestCase("? ")]
        public void Profile_ContainsEqualNameValues(string name)
        {
            var profile1 = new Profile(name, DEFAULT_PROCESS, []);
            var profile2 = new Profile(name, DEFAULT_PROCESS, []);
            var profile3 = new Profile($" {name}\t", DEFAULT_PROCESS, []);

            Assert.That(profile1.Name, Is.EqualTo(name.Trim()));
            Assert.That(profile1, Is.EqualTo(profile2));
            Assert.That(profile1, Is.EqualTo(profile3));
        }

        [TestCase("")]
        [TestCase("a")]
        [TestCase("X")]
        public void Profile_ContainsEqualProcessValues(string process)
        {
            var profile1 = new Profile(DEFAULT_NAME, process, []);
            var profile2 = new Profile(DEFAULT_NAME, process, []);
            var profile3 = new Profile(DEFAULT_NAME, $" {process}\t", []);

            Assert.That(profile1.ProcessToStart, Is.EqualTo(process.Trim()));
            Assert.That(profile1, Is.EqualTo(profile2));
            Assert.That(profile1, Is.EqualTo(profile3));
        }

        [TestCase()]
        [TestCase("a")]
        [TestCase("X")]
        public void Profile_ContainsEqualArgumentValues(params string[] arguments)
        {
            var profile1 = new Profile(DEFAULT_NAME, DEFAULT_PROCESS, arguments);
            var profile2 = new Profile(DEFAULT_NAME, DEFAULT_PROCESS, arguments);

            Assert.That(profile1.Arguments, Is.EquivalentTo(arguments));
            Assert.That(profile1, Is.EqualTo(profile2));
        }

        [TestCase("a", "A")]
        [TestCase("X", "Y")]
        public void Profile_ContainsDifferentNameValues(string name1, string name2)
        {
            var profile1 = new Profile(name1, DEFAULT_PROCESS, []);
            var profile2 = new Profile(name2, DEFAULT_PROCESS, []);

            Assert.That(name1.Trim(), Is.Not.EqualTo(name2.Trim()), "Test is not correctly setup!");
            Assert.That(profile1.Name, Is.Not.EqualTo(name2.Trim()));
            Assert.That(profile1, Is.Not.EqualTo(profile2));
        }

        [TestCase("", "?")]
        [TestCase("a", "A")]
        [TestCase("X", "Y")]
        public void Profile_ContainsDifferentProcessValues(string process1, string process2)
        {
            var profile1 = new Profile(DEFAULT_NAME, process1, []);
            var profile2 = new Profile(DEFAULT_NAME, process2, []);

            Assert.That(process1.Trim(), Is.Not.EqualTo(process2.Trim()), "Test is not correctly setup!");
            Assert.That(profile1.ProcessToStart, Is.Not.EqualTo(process2.Trim()));
            Assert.That(profile1, Is.Not.EqualTo(profile2));
        }

        [TestCase("", " ")]
        [TestCase("a", "A")]
        [TestCase("X", "Y")]
        [TestCase("test", "")]
        public void Profile_ContainsDifferentArgumentValues(string argument1, string argument2)
        {
            var profile1 = new Profile(DEFAULT_NAME, DEFAULT_PROCESS, [argument1]);
            var profile2 = new Profile(DEFAULT_NAME, DEFAULT_PROCESS, [argument2]);
            var profile3 = new Profile(DEFAULT_NAME, DEFAULT_PROCESS, [argument1, argument1]);
            var profile4 = new Profile(DEFAULT_NAME, DEFAULT_PROCESS, [argument1, argument2]);
            var profile5 = new Profile(DEFAULT_NAME, DEFAULT_PROCESS, []);

            Assert.That(argument1, Is.Not.EqualTo(argument2), "Test is not correctly setup!");

            Assert.That(profile1.Arguments, Is.Not.EquivalentTo(argument2));
            Assert.That(profile1, Is.Not.EqualTo(profile2));

            Assert.That(profile1.Arguments, Is.Not.EquivalentTo(profile3.Arguments));
            Assert.That(profile1, Is.Not.EqualTo(profile3));

            Assert.That(profile1.Arguments, Is.Not.EquivalentTo(profile4.Arguments));
            Assert.That(profile1, Is.Not.EqualTo(profile4));

            Assert.That(profile1.Arguments, Is.Not.EquivalentTo(profile5.Arguments));
            Assert.That(profile1, Is.Not.EqualTo(profile5));
        }
    }
}
