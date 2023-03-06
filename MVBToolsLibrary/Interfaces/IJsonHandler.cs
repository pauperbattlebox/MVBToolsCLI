using DataAccessLibrary.Models;

namespace MVBToolsLibrary.Json
{
    public interface IJsonHandler
    {
        string ReadFileFromJson(string filename);
    }
}