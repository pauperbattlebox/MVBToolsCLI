using MVBToolsLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVBToolsCLI
{
    public class ConsoleWriter : IConsoleWriter
    {
        public void WriteLineToConsole(string message)
        {
            Console.WriteLine( message );
        }
    }
}
