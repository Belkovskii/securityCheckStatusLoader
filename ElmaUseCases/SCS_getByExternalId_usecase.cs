using Newtonsoft.Json;
using System.Text;

namespace SecurityCheckStatusLoader.ElmaUseCases
{
    public static class SCS_getByExternalId_usecase
    {
        async public static Task<string> SCS_getByExternalId(string externalId, string host, HttpClient httpClient)
        {
            var payload = new
            {
                filter = new
                {
                    tf = new
                    {
                        SecurityCheckGUID = externalId
                    }
                },
                from = 0,
                size = 1
            };

            var jsonSerializerSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                Formatting = Formatting.None
            };
            string payloadJson = JsonConvert.SerializeObject(payload, jsonSerializerSettings);
            var url = $"{host}pub/v1/app/_clients/SecurityCheckStatus/list";
            var content = new StringContent(payloadJson, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await httpClient.PostAsync(url, content);
            Console.WriteLine(url);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            else
            {
                return "no value";
            }
        }
    }
}
