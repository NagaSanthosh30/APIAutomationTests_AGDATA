using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;

namespace APIAutomationTests_AGDATA.Helpers
{
    public class SchemaValidator
    {
        public bool ValidateSchema(string jsonResponse, string jsonSchema)
        {
            JSchema schema = JSchema.Parse(jsonSchema);

            try
            {
                if (jsonResponse.Trim().StartsWith("["))
                {
                    // If the JSON is an array
                    JArray responseArray = JArray.Parse(jsonResponse);
                    return responseArray.IsValid(schema, out IList<string> errorMessages);
                }
                else
                {
                    // If the JSON is an object
                    JObject responseObject = JObject.Parse(jsonResponse);
                    return responseObject.IsValid(schema, out IList<string> errorMessages);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Schema validation error: {ex.Message}");
                return false;
            }
        }
    
    }
}
