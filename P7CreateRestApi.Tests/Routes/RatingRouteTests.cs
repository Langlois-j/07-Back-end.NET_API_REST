using Dot.Net.WebApi.DTOs;
using P7CreateRestApi.Tests.Helpers;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace P7CreateRestApi.Tests.Routes
{
    public class RatingRouteTests : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _client;

        public RatingRouteTests(CustomWebApplicationFactory factory)
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
            var response = await _client.GetAsync("/Rating/list");
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task GetAll_ShouldReturn200_WithUserToken()
        {
            SetToken("User");
            var response = await _client.GetAsync("/Rating/list");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetById_ShouldReturn401_WhenNoToken()
        {
            var response = await _client.GetAsync("/Rating/1");
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task GetById_ShouldReturn404_WhenNotFound()
        {
            SetToken("User");
            var response = await _client.GetAsync("/Rating/999");
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task Post_ShouldReturn401_WhenNoToken()
        {
            var response = await _client.PostAsync("/Rating/validate",
                JsonBody(new RatingDTO { MoodysRating = "Aaa", SandPRating = "AAA", FitchRating = "AAA" }));
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task Post_ShouldReturn403_WithUserRole()
        {
            SetToken("User");
            var response = await _client.PostAsync("/Rating/validate",
                JsonBody(new RatingDTO { MoodysRating = "Aaa", SandPRating = "AAA", FitchRating = "AAA" }));
            Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
        }

        [Fact]
        public async Task Post_ShouldReturn400_WhenModelInvalid()
        {
            SetToken("Admin");
            var response = await _client.PostAsync("/Rating/validate",
                JsonBody(new RatingDTO { MoodysRating = "   ", SandPRating = "   ", FitchRating = "   " }));
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Post_ShouldReturn201_WithAdminToken()
        {
            SetToken("Admin");
            var response = await _client.PostAsync("/Rating/validate",
                JsonBody(new RatingDTO { MoodysRating = "Aaa", SandPRating = "AAA", FitchRating = "AAA" }));
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        [Fact]
        public async Task Put_ShouldReturn401_WhenNoToken()
        {
            var response = await _client.PutAsync("/Rating/update/1",
                JsonBody(new RatingDTO { MoodysRating = "Aaa", SandPRating = "AAA", FitchRating = "AAA" }));
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task Put_ShouldReturn403_WithUserRole()
        {
            SetToken("User");
            var response = await _client.PutAsync("/Rating/update/1",
                JsonBody(new RatingDTO { MoodysRating = "Aaa", SandPRating = "AAA", FitchRating = "AAA" }));
            Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
        }

        [Fact]
        public async Task Put_ShouldReturn404_WhenNotFound()
        {
            SetToken("Admin");
            var response = await _client.PutAsync("/Rating/update/999",
                JsonBody(new RatingDTO { MoodysRating = "Aaa", SandPRating = "AAA", FitchRating = "AAA" }));
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task Delete_ShouldReturn401_WhenNoToken()
        {
            var response = await _client.DeleteAsync("/Rating/1");
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task Delete_ShouldReturn403_WithUserRole()
        {
            SetToken("User");
            var response = await _client.DeleteAsync("/Rating/1");
            Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
        }

        [Fact]
        public async Task Delete_ShouldReturn404_WhenNotFound()
        {
            SetToken("Admin");
            var response = await _client.DeleteAsync("/Rating/999");
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task Delete_ShouldReturn204_WhenFound()
        {
            SetToken("Admin");
            await _client.PostAsync("/Rating/validate",
                JsonBody(new RatingDTO { MoodysRating = "Aaa", SandPRating = "AAA", FitchRating = "AAA" }));
            var response = await _client.DeleteAsync("/Rating/1");
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }
    }
}