

namespace MVBToolsLibrary.Json
{
    public class JsonHandler : IJsonHandler
    {        
        public string ReadFileFromJson(string filename)
        {
            using (
                StreamReader r = new StreamReader(filename))
            {
                string json = r.ReadToEnd();

                return json;                
            }
        }
        
    }
}
