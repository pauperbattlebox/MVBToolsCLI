using DataAccessLibrary.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVBToolsLibrary.Json
{
    public class EditionCardsJson
    {
        public EditionCardsModel Deserialize(string json)
        {
            var output = JsonConvert.DeserializeObject<EditionCardsModel>(json);
            Console.WriteLine(output);

            return output;
        }
    }
}
