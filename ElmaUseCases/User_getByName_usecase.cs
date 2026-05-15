using DocumentFormat.OpenXml.Wordprocessing;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net;
using System.Text;
using System.Text.Json;

namespace SecurityCheckStatusLoader.ElmaUseCases
{
    public class Operation
    {
        public string field { get; set; }
        public object value { get; set; }
    }
    public static class User_getByName_usecase
    {
        public static string User_getByName(
            string lastName,
            string firstName,
            string host,
            HttpClient httpClient,
            string token
        )
        {
            var url = $"{host}api/worker/query/system/users/search";
            var payload = new
            {
                filter = new
                {
                    and = new[]
                    {
                        new
                        {
                            and = new object[]
                            {
                                new { like = new object[] { new { field = "__name" }, new { @const = lastName } } },
                                new { like = new object[] { new { field = "__name" }, new { @const = firstName } } },
                                new { eq   = new object[] { new { field = "__status" }, new { @const = 2 } } },
                                new { eq   = new object[] { new { field = "__deletedAt" }, null } }
                            }
                        }
                    }
                },
                offset = 0,
                limit = 1,
                order = Array.Empty<object>()
            };
            var jsonSerializerSettings = new JsonSerializerSettings{Formatting = Formatting.None};
            string payloadJson = JsonConvert.SerializeObject(payload, jsonSerializerSettings);
            var calloutManager = new CalloutManager(httpClient, url, payloadJson, token, HttpMethod.Put);
            var response = calloutManager.SendRequest(out HttpStatusCode statusCode);
            if (statusCode == HttpStatusCode.OK)
            {
                return response;
            }
            else
            {
                return "user not found";
            }

        }
    }
}
