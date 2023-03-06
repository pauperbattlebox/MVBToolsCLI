using DataAccessLibrary.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MVBToolsLibrary.Json
{
    public class JsonHandler : IJsonHandler
    {
        //methods       
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
