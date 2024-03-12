using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hCMD
{
    public class TestArguments
    {
        public void CommandLineArguments(string[] args)
        {
            Runner.Main(args);
            Runner.Main(new string[] {"jotain"});
            Console.WriteLine("moimoimoi");

        }
    }
}
