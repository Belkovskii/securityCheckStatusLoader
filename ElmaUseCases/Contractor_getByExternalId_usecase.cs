using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace SecurityCheckStatusLoader.ElmaUseCases
{
    public static class Contractor_getByExternalId_usecase
    {
        async public static Task<string> Contractor_getByExternalId(string externalId, string host, HttpClient httpClient)
        {
            var payload = new
            {
                filter = new
                {
                    tf = new
                    {
                        ClientExternalGUID = externalId
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
            var url = $"{host}pub/v1/app/_clients/_companies/list";
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
