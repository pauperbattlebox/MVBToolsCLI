using DataAccessLibrary;
using Microsoft.Extensions.Configuration;
using MVBToolsLibrary.Json;
using MVBToolsLibrary.Endpoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLibrary.Models;
using System.Net;
using System.Data.SqlClient;
using MVBToolsCLI.Interfaces;
using DataAccessLibrary.Models.Interfaces;

namespace MVBToolsCLI
{
    public class EditionLogic
    {
                
        public static void ReadAllEditionsFromDb(SqlCrud sql)
        {
            var rows = sql.GetAllEditions();

            foreach (var row in rows)
            {
                Console.WriteLine($"{row.CsName} - {row.MtgJsonCode}");
            }
        }

        private static void ReadEditionFromJsonFile(SqlCrud sql, int editionId)
        {
            var edition = sql.GetCardsByEdition(editionId);

            Console.WriteLine($"{edition.Edition.Id} - {edition.Edition.CsName}");

            foreach (var card in edition.Cards)
            {
                Console.WriteLine($"{card.Name}");
            }

        }
        public static string GetMVBEditionEndpoint(int editionId)
        {
            var endpoint = Factory.CreateMvbEndpoint();

            return endpoint.EditionById(editionId);
        }

        public static void AddEditionToDb(SqlCrud sql, EditionModel editionModel)
        {
            sql.CreateSet(editionModel);
        }        

        //public static void JsonOperations(JsonObj jsonObj, string endpoint)
        //{            
        //    string json = jsonObj.GetApiUrl(endpoint);

        //    //Console.WriteLine(json);

        //    var model = jsonObj.Deserialize(json);

        //    SqlCrud sqlConnection = new SqlCrud(GetConnectionString());

        //    AddEdition(sqlConnection, model);

        //Console.Write("Card to find: ");
        //string cardToFind = Console.ReadLine();

        //Console.Write("Set to search: ");
        //string setToSearch = Console.ReadLine();

        //JsonObj jsonObj = new JsonObj($@"C:\Users\gunho\Desktop\programming\mvb-auto\json\{setToSearch}.json");

        //JToken jToken = jsonObj.ReadJsonFromFile(jsonObj);

        //List<CardModel> modelList = jsonObj.MatchByName(jToken, cardToFind);

        //foreach (CardModel model in modelList)
        //{
        //    Console.WriteLine($"{model.Name}, {model.MtgjsonId}");
        //}
        //}

    }
}
