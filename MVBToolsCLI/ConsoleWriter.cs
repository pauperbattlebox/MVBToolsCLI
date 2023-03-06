using MVBToolsLibrary.Interfaces;

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
