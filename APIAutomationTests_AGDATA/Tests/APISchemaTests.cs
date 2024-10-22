using APIAutomationTests_AGDATA.Tests;
using APIAutomationTests_AGDATA.Helpers;

namespace APIAutomationTests_AGDATA.Tests
{
    public class APISchemaTests
    {

        private readonly ApiClient _apiClient;
        private readonly SchemaValidator _schemaValidator;

        public APISchemaTests()
        {
            var config = ConfigurationLoader.LoadConfig();
            _apiClient = new ApiClient(config.BaseUrl);
            _schemaValidator = new SchemaValidator();
        }

        [Fact]
        public void GetUser_ShouldReturnValidUser()
        {
            // Make a GET request
            var response = _apiClient.Get("/posts");

            string userSchema = File.ReadAllText(Directory.GetCurrentDirectory() + "\\ExpectedSchemas\\GetSchema.json");

            // Assert the response
            Assert.True(response.IsSuccessStatusCode);
            // Assert that the response schema matches the expected schema
            bool isValid = _schemaValidator.ValidateSchema(response.Content, userSchema);

            Assert.True(isValid, "API response schema does not match the expected schema.");
        }

    }
}