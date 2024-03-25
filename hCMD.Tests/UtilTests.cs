namespace hCMD.Tests
{
    public class UtilTests
    {
        [TestCase(@"C:\WINDOWS", @"C:\Users\Tester")]
        [TestCase(@"C:\WINDOWS", @"C:/Users/Tester")]
        [TestCase(@"C:\WINDOWS", @"C:\Users\Tester\")]
        [TestCase(@"C:\WINDOWS;C:\Users\Tester", @"C:\WINDOWS\System32")]
        public void AppendToPathString_ReturnsTrue(string environmentPathData, string pathToAppend)
        {
            var result = Utils.AppendToPathString(environmentPathData, pathToAppend);

            Assert.That(result, Does.StartWith($"{environmentPathData};"));
            Assert.That(result, Does.EndWith($";{pathToAppend}"));
        }

        [TestCase(@"C:\WINDOWS", "")]
        [TestCase(@"C:\WINDOWS", ";")]
        [TestCase(@"C:\WINDOWS", @";C:\windows")]
        [TestCase(@"C:\WINDOWS", @"C:\WINDOWS;")]
        [TestCase(@"C:\WINDOWS", @"C:\Users\Tester;C:\WINDWOS\System32")]
        public void AppendToPathString_ThrowsArgumentException(string environmentPathData, string pathToAppend)
        {
            Assert.Throws(
                typeof(ArgumentException),
                () => Utils.AppendToPathString(environmentPathData, pathToAppend)
            );
        }

        [TestCase(@"C:\WINDOWS", @"c:\windows")]
        [TestCase(@"C:\WINDOWS", @"C:\windows\")]
        [TestCase(@"C:\WINDOWS\", @"C:\WINDOWS")]
        [TestCase(@"C:\WINDOWS;C:\Users\Tester", "C:/Users/Tester")]
        [TestCase(@"C:\WINDOWS;C:/Users/Tester/", "C:/users/tester/")]
        [TestCase(@"C:\WINDOWS;C:\WINDOWS\System32;C:\Users\Tester", @"C:\WINDOWS\system32")]
        public void IncludedInPathString_ReturnsTrue(string environmentPathData, string pathToCheck)
        {
            Assert.That(Utils.IncludedInPathString(environmentPathData, pathToCheck), Is.True);
        }

        [TestCase(@"C:\WINDOWS", "")]
        [TestCase(@"C:\WINDOWS", "C:/")]
        [TestCase(@"C:\WINDOWS", @"C:\")]
        [TestCase(@"C:\WINDOWS", "C:/WINDOW")]
        [TestCase(@"C:\WINDOWS", "C:/WINDOWS//")]
        [TestCase(@"C:\WINDOWS", @"C:\windows\\")]
        [TestCase(@"C:WINDOWS", @"C:\Users\Tester")]
        public void IncludedInPathString_ReturnsFalse(string environmentPathData, string pathToCheck)
        {
            Assert.That(Utils.IncludedInPathString(environmentPathData, pathToCheck), Is.False);
        }
    }
}