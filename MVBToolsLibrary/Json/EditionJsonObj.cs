using DataAccessLibrary.Models;
using MVBToolsCLI.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVBToolsLibrary.Json
{
    public class EditionJsonObj
    {
        public EditionModel Deserialize(string json)
        {
            var output = JsonConvert.DeserializeObject<EditionModel>(json);
            Console.WriteLine(output);

            return output;
        }
    }
}
