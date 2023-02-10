using DataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MVBToolsCLI.Interfaces
{
    public interface IJsonObj
    {
        public EditionModel Deserialize(string json);
    }
}
