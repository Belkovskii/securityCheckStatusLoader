using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityCheckStatusLoader.ElmaUseCases
{
    public static class Status_getByDevName_usecase
    {
        async public static Task<string> Status_getByDevName(string devName, HttpClient httpClient, string host)
        {
            var payload = new
            {
                filter = new
                {
                    tf = new
                    {
                        DeveloperName = devName
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
            var url = $"{host}pub/v1/app/_system_catalogs/StatusESS/list";
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
