using DataAccessLibrary.Models;

namespace MVBToolsLibrary.Json
{
    public interface IFileReader
    {
        string ReadFile(string filename);
    }
}