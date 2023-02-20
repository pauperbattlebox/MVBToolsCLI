namespace MVBToolsLibrary.Endpoint.Interfaces
{
    public interface IScryfallEndpoint
    {
        string _baseUrl { get; }

        string AllCards();
        string CardById(string id);
    }
}