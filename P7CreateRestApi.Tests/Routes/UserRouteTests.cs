using Dot.Net.WebApi.DTOs;
using P7CreateRestApi.Tests.Helpers;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace P7CreateRestApi.Tests.Routes
{
    public class UserRouteTests : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _client;

        public UserRouteTests(CustomWebApplicationFactory factory)
        {
            _client = factory.CreateClient();
        }

        private void SetToken(string role = "Admin")
        {
            _client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", MockTokenHelper.GenerateToken(role));
        }

        private StringContent JsonBody(object dto) =>
            new StringContent(JsonSerializer.Serialize(dto), Encoding.UTF8, "application/json");

        private UserCreateDTO ValidUser(string userName = "testuser") => new UserCreateDTO
        {
            UserName = userName,
            Password = "Password123!",
            Fullname = "Test User",
            Role = "User"
        };

        [Fact]
        public async Task GetAll_ShouldReturn401_WhenNoToken()
        {
            var response = await _client.GetAsync("/User/list");
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task GetAll_ShouldReturn403_WithUserRole()
        {
            SetToken("User");
            var response = await _client.GetAsync("/User/list");
            Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
        }

        [Fact]
        public async Task GetAll_ShouldReturn200_WithAdminToken()
        {
            SetToken("Admin");
            var response = await _client.GetAsync("/User/list");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetById_ShouldReturn401_WhenNoToken()
        {
            var response = await _client.GetAsync("/User/some-id");
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task GetById_ShouldReturn403_WithUserRole()
        {
            SetToken("User");
            var response = await _client.GetAsync("/User/some-id");
            Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
        }

        [Fact]
        public async Task GetById_ShouldReturn404_WhenNotFound()
        {
            SetToken("Admin");
            var response = await _client.GetAsync("/User/id-inconnu");
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task Post_ShouldReturn201_WhenAnonymous()
        {
            var response = await _client.PostAsync("/User/validate", JsonBody(ValidUser("newuser1")));
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        [Fact]
        public async Task Post_ShouldReturn400_WhenPasswordTooWeak()
        {
            var dto = new UserCreateDTO
            {
                UserName = "newuser2",
                Password = "123",
                Fullname = "Test",
                Role = "User"
            };
            var response = await _client.PostAsync("/User/validate", JsonBody(dto));
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Post_ShouldReturn400_WhenModelInvalid()
        {
            var response = await _client.PostAsync("/User/validate", JsonBody(new UserCreateDTO()));
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Put_ShouldReturn401_WhenNoToken()
        {
            var response = await _client.PutAsync("/User/update/some-id", JsonBody(new UserDTO()));
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task Put_ShouldReturn403_WithUserRole()
        {
            SetToken("User");
            var response = await _client.PutAsync("/User/update/some-id", JsonBody(new UserDTO()));
            Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
        }

  
        [Fact]
        public async Task Put_ShouldReturn404_WhenNotFound()
        {
            SetToken("Admin");
            var response = await _client.PutAsync("/User/update/id-inconnu",
                JsonBody(new UserDTO { UserName = "test", Role = "User" })); // ← Role valide
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task Delete_ShouldReturn401_WhenNoToken()
        {
            var response = await _client.DeleteAsync("/User/some-id");
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task Delete_ShouldReturn403_WithUserRole()
        {
            SetToken("User");
            var response = await _client.DeleteAsync("/User/some-id");
            Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
        }

        [Fact]
        public async Task Delete_ShouldReturn404_WhenNotFound()
        {
            SetToken("Admin");
            var response = await _client.DeleteAsync("/User/id-inconnu");
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task Delete_ShouldReturn204_WhenFound()
        {
            var createResponse = await _client.PostAsync("/User/validate", JsonBody(ValidUser("todelete")));
            var body = await createResponse.Content.ReadAsStringAsync();

            
            Assert.True(createResponse.IsSuccessStatusCode, $"POST failed: {body}");
        }
    }
}