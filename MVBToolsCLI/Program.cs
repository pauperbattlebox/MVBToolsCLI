using Microsoft.Extensions.Configuration;
using System;
using DataAccessLibrary;
using DataAccessLibrary.Models;
using Newtonsoft.Json.Linq;
using MVBToolsLibrary.Endpoint;
using MVBToolsLibrary.Json;
using System.Data.SqlClient;


namespace MVBToolsCLI
{
    class Program
    {
        static void Main(string[] args)
        {
            SqlCrud sqlConnection = new SqlCrud(Utils.GetConnectionString());
            //Console.WriteLine("What set would you like to add: ");

            //string editionId = Console.ReadLine();

            //EditionLogic.AddNewEditionToDb(Int32.Parse(editionId), sqlConnection);

            //CardLogic.AddMultipleCardsToDb(Int32.Parse(editionId), sqlConnection);

            PriceLogic.UpdateScryfallPriceInDb(sqlConnection, "a742e23c-1991-4dce-b670-dea92a1cf4ec");

            Console.WriteLine("That's the end");

            Console.ReadLine();
        }        
    }
}