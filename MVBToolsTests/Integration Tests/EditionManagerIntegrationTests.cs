using Bogus;
using MVBToolsLibrary.Models;
using RestSharp;
using System.Diagnostics;

namespace MVBToolsTests.Integration_Tests
{
    [TestClass]
    public class EditionManagerIntegrationTests
    {
        private readonly MvbApiMockServer _mvbApiMockServer = new();

        [TestMethod]
        public async Task TestGetEditionFromApi()
        {
            EditionModel edition = GenerateFakeEditionModel();
            var client = new RestClient("http://localhost:9876");


            _mvbApiMockServer.Start();
            _mvbApiMockServer.ReturnEdition(edition);            
            RestRequest request = new RestRequest($"/edition/{edition.CsId}", Method.Get);
            RestResponse response = await client.ExecuteAsync(request);

            Debug.WriteLine($"{response.StatusCode}");
            Debug.WriteLine($"{response.ContentType}");
            Debug.WriteLine($"{response.Content}");

            //Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
            //Assert.AreEqual(response.ContentType, "application/json");
            //Assert.IsTrue(response.Content.Contains(edition.CsId.ToString()));

            _mvbApiMockServer.Dispose();
        }

        private EditionModel GenerateFakeEditionModel()
        {
            var faker = new Faker<EditionModel>()
                .RuleFor(x => x.CsId, f => f.Random.Int(1, 1000))
                .RuleFor(x => x.CsName, f => f.Random.Words())
                .RuleFor(x => x.MtgJsonCode, f => f.Lorem.Word());

            var edition = faker.Generate();

            return edition;
        }
    }
}
