

namespace MVBToolsLibrary.Endpoint
{
    public class MvbEndpoint : Endpoint
    {
        public override string _baseUrl { get; } = "https://www.multiversebridge.com/api/v1";

        public string EditionById(int editionId)
        {
            return $"{_baseUrl}/sets/cs/{editionId}";
        }

        public string AllEditions()
        {
            return $"{_baseUrl}/sets";
        }

        public string EditionByMtgJsonCode(string mtgJsonCode)
        {
            return $"{_baseUrl}/sets/mtgjson/{mtgJsonCode}";
        }

        public string AllCards()
        {
            return $"{_baseUrl}/cards";
        }

        public string CardById(int csId)
        {
            return $"{_baseUrl}/cards/cs/{csId}";
        }

        public string CardByMtgJsonId(string mtgJsonId)
        {
            return $"{_baseUrl}/cards/mtgjson/{mtgJsonId}";
        }

        public string CardByScryfallId(string scryfallId)
        {
            return $"{_baseUrl}/cards/scryfall/{scryfallId}";
        }

    }
}
