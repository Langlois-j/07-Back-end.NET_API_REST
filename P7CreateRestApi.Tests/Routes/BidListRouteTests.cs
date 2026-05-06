using Dot.Net.WebApi.DTOs;
using P7CreateRestApi.Tests.Helpers;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace P7CreateRestApi.Tests.Routes
{
    public class BidListRouteTests : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _client;

        public BidListRouteTests(CustomWebApplicationFactory factory)
        {
            _client = factory.CreateClient();
        }

        private void SetToken(string role = "User")
        {
            _client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", MockTokenHelper.GenerateToken(role));
        }

        private StringContent JsonBody(object dto) =>
            new StringContent(JsonSerializer.Serialize(dto), Encoding.UTF8, "application/json");

        [Fact]
        public async Task GetAll_ShouldReturn401_WhenNoToken()
        {
            var response = await _client.GetAsync("/BidList/list");
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task GetAll_ShouldReturn200_WithUserToken()
        {
            SetToken("User");
            var response = await _client.GetAsync("/BidList/list");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetById_ShouldReturn401_WhenNoToken()
        {
            var response = await _client.GetAsync("/BidList/1");
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task GetById_ShouldReturn404_WhenNotFound()
        {
            SetToken("User");
            var response = await _client.GetAsync("/BidList/999");
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task Post_ShouldReturn401_WhenNoToken()
        {
            var response = await _client.PostAsync("/BidList/validate",
                JsonBody(new BidListDTO { Account = "A", BidType = "T" }));
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        

        [Fact]
        public async Task Post_ShouldReturn400_WhenModelInvalid()
        {
            SetToken("Admin");
            var response = await _client.PostAsync("/BidList/validate",
                JsonBody(new BidListDTO()));
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Post_ShouldReturn201_WithAdminToken()
        {
            SetToken("Admin");
            var response = await _client.PostAsync("/BidList/validate",
                JsonBody(new BidListDTO { Account = "TestAccount", BidType = "TestType" }));
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        [Fact]
        public async Task Put_ShouldReturn401_WhenNoToken()
        {
            var response = await _client.PutAsync("/BidList/update/1",
                JsonBody(new BidListDTO { Account = "A", BidType = "T" }));
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }


        [Fact]
        public async Task Put_ShouldReturn404_WhenNotFound()
        {
            SetToken("Admin");
            var response = await _client.PutAsync("/BidList/update/999",
                JsonBody(new BidListDTO { Account = "A", BidType = "T" }));
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task Delete_ShouldReturn401_WhenNoToken()
        {
            var response = await _client.DeleteAsync("/BidList/1");
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task Delete_ShouldReturn404_WhenNotFound()
        {
            SetToken("Admin");
            var response = await _client.DeleteAsync("/BidList/999");
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task Delete_ShouldReturn204_WhenFound()
        {
            SetToken("Admin");
            await _client.PostAsync("/BidList/validate",
                JsonBody(new BidListDTO { Account = "ToDelete", BidType = "T" }));

            var response = await _client.DeleteAsync("/BidList/1");
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }
    }
}