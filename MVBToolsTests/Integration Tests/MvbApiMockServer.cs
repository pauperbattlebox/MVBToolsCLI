using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;

namespace MVBToolsTests.Integration_Tests
{
    public class MvbApiMockServer: IDisposable
    {
        private WireMockServer _server;

        public void Start()
        {
            _server = WireMockServer.Start(9876);
        }

        public void ReturnEdition(int editionId)
        {

            string jsonResponse = @"{""cs_id"": 965,
                                    ""cs_name"": ""Archenemy: Nicol Bolas"",
                                    ""mtgjson_code"": ""E01""}";

            _server.Given(
                Request.Create().WithPath($"/edition/{editionId}").UsingGet()
            )
            .RespondWith(
                Response.Create()
                .WithStatusCode(200)
                .WithHeader("Content-Type", "application/json")
                .WithBody(jsonResponse)
                );
        }

        public void Dispose()
        {
            _server.Stop();
            _server.Dispose();
        }
    }
}