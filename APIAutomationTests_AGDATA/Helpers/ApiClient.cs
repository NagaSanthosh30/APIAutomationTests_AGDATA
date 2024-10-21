using Newtonsoft.Json;
using RestSharp;

namespace AGDATA_APIAutomationTests.Helpers
{
    public class ApiClient
    {
        private readonly RestClient _client;

        public ApiClient(string baseUrl)
        {
            _client = new RestClient(baseUrl);
        }

        // Generic method for GET requests
        public RestResponse Get(string resource, object parameters = null)
        {
            var request = new RestRequest(resource, Method.Get);
            if (parameters != null)
                request.AddJsonBody(parameters);

            return ExecuteRequest(request);
        }

        // Generic method for POST requests
        public RestResponse Post(string resource, object body)
        {
            var request = new RestRequest(resource, Method.Post);
            request.AddJsonBody(body);
            return ExecuteRequest(request);
        }

        // Generic method for PUT requests
        public RestResponse Put(string resource, object body)
        {
            var request = new RestRequest(resource, Method.Put);
            request.AddJsonBody(body);
            return ExecuteRequest(request);
        }

        // Generic method for DELETE requests
        public RestResponse Delete(string resource)
        {
            var request = new RestRequest(resource, Method.Delete);
            return ExecuteRequest(request);
        }


        // Execute the request and handle exceptions
        private RestResponse ExecuteRequest(RestRequest request)
        {
            try
            {
                var response = _client.Execute(request);
                if (!response.IsSuccessful)
                {
                    Console.WriteLine($"Request failed: {response.StatusCode}");
                }
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception occurred: {ex.Message}");
                throw;
            }
        }

        // Deserialize JSON to object
        public T DeserializeResponse<T>(RestResponse response)
        {
            return JsonConvert.DeserializeObject<T>(response.Content);
        }
    }
}
