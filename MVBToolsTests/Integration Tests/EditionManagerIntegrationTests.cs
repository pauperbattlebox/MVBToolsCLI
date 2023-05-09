using RestSharp;
using System.Diagnostics;
using System.Net;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;

namespace MVBToolsTests.Integration_Tests
{
    [TestClass]
    public class EditionManagerIntegrationTests
    {
        private readonly MvbApiMockServer _mvbApiMockServer = new();

        [TestMethod]
        public async Task TestGetEditionFromApi()
        {
            int editionId = 965;

            _mvbApiMockServer.Start();

            _mvbApiMockServer.ReturnEdition(editionId);
            

            var client = new RestClient("http://localhost:9876");
            RestRequest request = new RestRequest($"/edition/{editionId}", Method.Get);
            RestResponse response = await client.ExecuteAsync(request);

            Debug.WriteLine($"{response.StatusCode}");
            Debug.WriteLine($"{response.ContentType}");
            Debug.WriteLine($"{response.Content}");

            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
            Assert.AreEqual(response.ContentType, "application/json");
            Assert.IsTrue(response.Content.Contains(editionId.ToString()));

            _mvbApiMockServer.Dispose();
        }

    }
}
