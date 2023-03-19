﻿using DataAccessLibrary;
using DataAccessLibrary.Models;
using MVBToolsLibrary;
using MVBToolsLibrary.Interfaces;
using MVBToolsLibrary.Json;

namespace MVBToolsCLI
{
    public class Commands
    {        
        public static void AddNewEditionToDb(int editionId, SqlCrud sqlConnection, IConsoleWriter consoleWriter)
        {
            Edition edition = new Edition(consoleWriter);

            EditionModel editionToAdd = edition.GetEditionFromMvb(editionId);

            edition.AddEditionToDb(sqlConnection, editionToAdd, consoleWriter);
        }

        public static void AddCardsToDbByEdition(int editionId, SqlCrud sqlConnection, IConsoleWriter consoleWriter)
        {
            Card card = new Card(consoleWriter);

            EditionCardsModel model = card.GetCardsFromMVBAPI(editionId, consoleWriter);

            card.AddFilteredCardsToDb(sqlConnection, model);
        }

        public static void RefreshScryfallPriceInDb(string scryfallId, SqlCrud sqlConnection, IConsoleWriter consoleWriter)
        {
            Price price = new Price(consoleWriter);

            price.UpdateScryfallPriceInDb(scryfallId, sqlConnection);
        }

        public static void RefreshMVBPriceInDb(int csId, SqlCrud sqlConnection, IConsoleWriter consoleWriter)
        {
            Price price = new Price(consoleWriter);

            price.UpdateMVBPriceInDb(csId, sqlConnection);
        }

        public static void GetEditionsFromDb(SqlCrud sqlConnection, IConsoleWriter consoleWriter)
        {
            Edition edition = new Edition(consoleWriter);

            var editions = edition.ReadAllEditionsFromDb(sqlConnection);

            foreach(var editionModel in editions.Result)
            {
                Console.WriteLine($"{editionModel.CsName} - {editionModel.MtgJsonCode}");
            }
        }

        public static void GetCardFromDb(SqlCrud sqlConnection, int csId, IConsoleWriter consoleWriter)
        {
            Card card = new Card(consoleWriter);

            card.ReadCardFromDb(sqlConnection, csId);
        }

        public static void GetCardPriceFromDb(SqlCrud sqlConnection, int csId, IConsoleWriter consoleWriter)
        {
            Card card = new Card(consoleWriter);

            card.ReadCardPriceFromDb(sqlConnection, csId);
        }

        public static void AddCardsToDbFromJsonFile(SqlCrud sqlConnection, string fileName, IConsoleWriter consoleWriter, IFileReader filereader)
        {

            Card card = new Card(consoleWriter, filereader);

            var json = card.ReadCardsFromMvbJsonFile(fileName);

            foreach (MVBCardModel cardModel in json)
            {
                Card newCard = new Card(consoleWriter);

                newCard.AddCardToDb(sqlConnection, cardModel);
            }
        }
    }
}
