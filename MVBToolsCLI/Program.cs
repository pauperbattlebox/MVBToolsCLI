using Microsoft.Extensions.Configuration;
using System;
using DataAccessLibrary;
using DataAccessLibrary.Models;
using MVBToolsLibrary;
using Newtonsoft.Json.Linq;

namespace MVBToolsCLI
{
    class Program
    {
        static void Main(string[] args)
        {

            SqlCrud sqlConnection = new SqlCrud(GetConnectionString());
            
            JsonOperations();

            Console.ReadLine();
        }

        private static void AddSet(SqlCrud sql)
        {
            SetModel setModel = new SetModel()
            {
                CsId = 759,
                CsName = "7th Edition",
                MtgjsonCode = "7ED"
            };

            sql.CreateSet(setModel);
        }

        private static void ReadAllSets(SqlCrud sql)
        {
            var rows = sql.GetAllSets();

            foreach (var row in rows)
            {
                Console.WriteLine($"{row.CsName} - {row.MtgjsonCode}"); 
            }
        }

        private static void ReadSet(SqlCrud sql, int setId)
        {
            var set = sql.GetSetsAndCardsInSet(setId);

            
            Console.WriteLine($"{set.Set.Id} - {set.Set.CsName}");

            foreach (var card in set.Cards)
            {
                Console.WriteLine($"{card.Name} - {card.Edition}");
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

        private static void JsonOperations()
        {
            JsonObj jsonObj = new JsonObj("yolo");
            string json = jsonObj.GetJson("https://www.multiversebridge.com/api/v1/cards/search?name=leyline&edition=Gatecrash&collector_number=41&mtgjson_code=GTC");

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