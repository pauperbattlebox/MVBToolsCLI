using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WireMock.Server;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using MVBToolsLibrary.Models.ProviderModels;

namespace MVBToolsTests.Integration_Tests.Mock_Api_Servers
{
    public class ScryfallApiMockServer: IDisposable
    {
        private WireMockServer _server;

        public void Start()
        {
            _server = WireMockServer.Start(1234);
        }

        public void ReturnPrice (ScryfallCardModel cardModel)
        {
            _server.Given(
                Request.Create().WithPath($"/{cardModel.ScryfallId}").UsingGet()
            )
                .RespondWith(
                Response.Create()
                .WithStatusCode(200)
                .WithHeader("Content-Type", "application/json")
                .WithBodyAsJson(cardModel)
                );
        }

        public void Dispose()
        {
            _server.Stop();
            _server.Dispose();
        }
    }
}
