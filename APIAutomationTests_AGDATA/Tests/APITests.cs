using APIAutomationTests_AGDATA.Helpers;
using APIAutomationTests_AGDATA.Models;
using System.Net;

namespace APIAutomationTests_AGDATA.Tests
{
    public class APITests
    {

        private readonly ApiClient _apiClient;

        public APITests()
        {
            var config = ConfigurationLoader.LoadConfig();
            _apiClient = new ApiClient(config.BaseUrl);
        }

        [Fact]
        public void GetUser_ShouldReturnValidUser()
        {
            // Make a GET request
            var response = _apiClient.Get("/posts");
            var user = _apiClient.DeserializeResponse<List<User>>(response);

            // Assert the response
            Assert.True(response.IsSuccessStatusCode);
            Assert.Equal(1, user[0].id);
            Assert.True(user[0].title.Length > 2);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public void PostUser_ShouldReturnValidUser()
        {
            // Make a POST request
            var userBody = new User
            {
                id = 1,
                userId = 2,
                body = "Test body",
                title = "Test title"
            };
            var response = _apiClient.Post("/posts", userBody);
            var user = _apiClient.DeserializeResponse<User>(response);

            // Assert the response
            Assert.True(response.IsSuccessStatusCode);
            Assert.Equal(2, user.userId);
            Assert.Equal("Test title",user.title);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        [Fact]
        public void PutUser_ShouldReturnValidUser()
        {
            // Make a PUT request
            var userBody = new User
            {
                id = 1,
                userId = 2,
                body = "Update Test body",
                title = "Update Test title"
            };
            var response = _apiClient.Put($"/posts/{userBody.id}", userBody);
            var user = _apiClient.DeserializeResponse<User>(response);

            // Assert the response
            Assert.True(response.IsSuccessStatusCode);
            Assert.Equal(2, user.userId);
            Assert.Equal("Update Test title", user.title);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public void DeleteUser_ShouldReturnValidUser()
        {
            // Make a Delete request
            var userBody = new User
            {
                id = 1
            };
            var response = _apiClient.Delete($"/posts/{userBody.id}");

            // Assert the response
            Assert.True(response.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public void PostUserComments_ShouldReturnValidUser()
        {
            // Make a POST request
            var userBody = new User
            {
                id = 1,
                userId = 2,
                body = "Test body",
                title = "Test title"
            };
            var response = _apiClient.Post($"/posts/{userBody.id}/comments", userBody);
            var user = _apiClient.DeserializeResponse<User>(response);

            // Assert the response
            Assert.True(response.IsSuccessStatusCode);
            Assert.Equal(2, user.userId);
            Assert.Equal("Test title", user.title);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        [Fact]
        public void GetUserWithId_ShouldReturnValidUser()
        {
            // Make a GET request
            var postId =1;
            var response = _apiClient.Get($"/comments?postId={postId}");
            var user = _apiClient.DeserializeResponse<List<User>>(response);

            // Assert the response
            Assert.True(response.IsSuccessStatusCode);
            Assert.Equal(1, user[0].id);
            Assert.True(user[0].body.Length > 2);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

    }
}