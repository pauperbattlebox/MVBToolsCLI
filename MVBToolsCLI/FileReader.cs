

namespace MVBToolsLibrary.Json
{
    public class FileReader : IFileReader
    {        
        public string ReadFile(string filename)
        {
            using (
                StreamReader r = new StreamReader(filename))
            {
                string text = r.ReadToEnd();

                return text;                
            }
        }
        
    }
}
