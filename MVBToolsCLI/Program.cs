using Microsoft.Extensions.Configuration;
using System;
using DataAccessLibrary;
using DataAccessLibrary.Models;

namespace MVBToolsCLI
{
    class Program
    {
        static void Main(string[] args)
        {

            SqlCrud sqlConnection = new SqlCrud(GetConnectionString());

            //ReadAllSets(sqlConnection);

            //ReadSet(sqlConnection, 1);

            

            AddSet(sqlConnection);

            Console.ReadLine();
        }

        private static void AddSet(SqlCrud sql)
        {
            SetsModel setsModel = new SetsModel()
            {
                CsId = 759,
                CsName = "7th Edition",
                MtgjsonCode = "7ED",
                CreatedOn = DateTime.Now
            };

            sql.CreateSet(setsModel);
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

            
            Console.WriteLine($"{set.Sets.Id} - {set.Sets.CsName}");

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
     
    }
}