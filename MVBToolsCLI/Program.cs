using Microsoft.Extensions.Configuration;
using System;
using DataAccessLibrary;
using DataAccessLibrary.Models;
using Newtonsoft.Json.Linq;
using MVBToolsLibrary.Endpoint;
using MVBToolsLibrary.Json;

namespace MVBToolsCLI
{
    class Program
    {
        static void Main(string[] args)
        {

            SqlCrud sqlConnection = new SqlCrud(GetConnectionString());            

            Console.WriteLine("That's the end");

            Console.ReadLine();
        }
        private static string GetEditionFromAPI(int editionId)
        {
            MvbEndpoint endpoint= new MvbEndpoint();
            

            return endpoint.EditionById(editionId);
        }

        private static void AddEdition(SqlCrud sql)
        {
            EditionModel editionModel = new EditionModel()
            {
                CsId = 759,
                CsName = "7th Edition",
                MtgJsonCode = "7ED"
            };

            sql.CreateSet(editionModel);
        }

        private static void ReadAllEditions(SqlCrud sql)
        {
            var rows = sql.GetAllEditions();

            foreach (var row in rows)
            {
                Console.WriteLine($"{row.CsName} - {row.MtgJsonCode}"); 
            }
        }

        private static void ReadSet(SqlCrud sql, int editionId)
        {
            var edition = sql.GetSetsAndCardsInSet(editionId);

            
            Console.WriteLine($"{edition.Edition.Id} - {edition.Edition.CsName}");

            foreach (var card in edition.Cards)
            {
                Console.WriteLine($"{card.Name}");
            }

        }

        private static string GetConnectionString(string connectionStringName = "Default")
        {
            string output = "";

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            var config = builder.Build();

            output = config.GetConnectionString(connectionStringName);

            return output;
        }

        private static void JsonOperations(string endpoint)
        {
            JsonObj jsonObj = new JsonObj("yolo");
            string json = jsonObj.GetJson(endpoint);

            Console.WriteLine(json);

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
        }

    }
}