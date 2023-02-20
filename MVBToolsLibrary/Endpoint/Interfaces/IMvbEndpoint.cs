namespace MVBToolsLibrary.Endpoint.Interfaces
{
    public interface IMvbEndpoint
    {
        string _baseUrl { get; }

        string AllCards();
        string AllEditions();
        string CardById(int csId);
        string CardByMtgJsonId(string mtgJsonId);
        string CardByScryfallId(string scryfallId);
        string EditionById(int editionId);
        string EditionByMtgJsonCode(string mtgJsonCode);
    }
}