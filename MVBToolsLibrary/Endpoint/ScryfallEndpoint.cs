

namespace MVBToolsLibrary.Endpoint
{
    public class ScryfallEndpoint : Endpoint
    {
        public override string _baseUrl { get; } = "https://api.scryfall.com";

        public string CardById(string id)
        {
            return $"{_baseUrl}/cards/{id}";
        }

        public string AllCards()
        {
            return $"{_baseUrl}/bulk-data/all_cards";
        }
    }
}
