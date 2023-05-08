using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WireMock;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;

namespace MVBToolsTests.Integration_Tests
{
    [TestClass]
    public class EditionManagerIntegrationTests
    {
        [TestMethod]
        public async Task TestGetEditionFromApi()
        {
            string jsonResponse = @"{
                                    ""cs_id"": 965,
                                    ""cs_name"": ""Archenemy: Nicol Bolas"",
                                    ""mtgjson_code"": ""E01""}";

            var server = WireMockServer.Start(9876);

            server.Given(
                Request.Create().WithPath("/edition").UsingGet()
            )
            .RespondWith(
                Response.Create()
                .WithStatusCode(200)
                .WithHeader("Content-Type", "application/json")
                .WithBody(jsonResponse)
                );            

            var client = new RestClient("http://localhost:9876");
            RestRequest request = new RestRequest("/edition", Method.Get);
            RestResponse response = await client.ExecuteAsync(request);

            Debug.WriteLine($"{response.StatusCode} - {response.ContentType} - {response.Content}");

            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
            Assert.AreEqual(response.ContentType, "application/json");
            Assert.AreEqual(response.Content, jsonResponse);

            server.Stop();
        }

    }
}
