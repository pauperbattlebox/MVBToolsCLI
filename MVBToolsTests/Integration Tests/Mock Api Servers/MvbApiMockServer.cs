using Bogus;
using MVBToolsLibrary.Models;
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

        public void ReturnEdition(EditionModel edition)
        {
            _server.Given(
                Request.Create().WithPath($"/edition/{edition.CsId}").UsingGet()
            )
            .RespondWith(
                Response.Create()
                .WithStatusCode(200)
                .WithHeader("Content-Type", "application/json")
                .WithBodyAsJson(edition)
                );
        }

        public void Dispose()
        {
            _server.Stop();
            _server.Dispose();
        }
    }
}