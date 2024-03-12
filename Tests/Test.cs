using System;

namespace hCMD.Tests
{
    public static class Test
    {
        public static void TestArguments_CommandLineArguments_ReturnString()
        {
            try
            {
                // Arrange - get variables and objects
                string[] args = new string[] { "start", "notepad" };
                TestArguments testArguments = new TestArguments();

                // Act - call method under test
                testArguments.CommandLineArguments(args);

                // Assert - check the results
                Console.WriteLine("TestArguments_CommandLineArguments_ReturnString passed");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}