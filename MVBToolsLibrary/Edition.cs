﻿using DataAccessLibrary.Models;
using DataAccessLibrary;
using MVBToolsLibrary.Endpoint;
using System.Text.Json;

namespace MVBToolsLibrary
{
    public class Edition
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

            Console.WriteLine($"{edition.Edition.CsId} - {edition.Edition.CsName}");

            foreach (var card in edition.Cards)
            {
                Console.WriteLine($"{card.Name}");
            }

        }
        public static string GetMVBEditionEndpoint(int editionId)
        {
            var endpoint = new MvbEndpoint();

            return endpoint.EditionById(editionId);
        }

        public static void AddEditionToDb(SqlCrud sql, EditionModel editionModel)
        {
            sql.CreateSet(editionModel);
        }

        public static EditionModel GetEditionFromMvb(int editionId)
        {
            string endpoint = GetMVBEditionEndpoint(editionId);

            string response = HttpClientFactory.CallEndpoint(endpoint);

            EditionModel output = JsonSerializer.Deserialize<EditionModel>(response);

            return output;
        }
    }
}