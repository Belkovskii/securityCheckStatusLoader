using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityCheckStatusLoader.ElmaUseCases
{
    public static class SCS_update_usecase
    {
        async public static Task<string> Update(
            HttpClient httpClient,
            string host,
            ScsUpdateCreateData recordData
         )
        {
            var url = $"{host}pub/v1/app/_clients/SecurityCheckStatus/{recordData.RecordId}/update";
            var payload = new
            {
                context = new
                {
                    SecurityCheckGUID = recordData.SecurityCheckGUID ?? null,
                    CheckDate = recordData.CheckDate.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss.fffZ"),
                    SecurotyUserId = !string.IsNullOrEmpty(recordData.SecurotyUserId)
                        ? new[] { recordData.SecurotyUserId }
                        : null,
                    ExternalCreatedByText = recordData.ExternalCreatedByText ?? null,
                    CRMClienId = recordData.CRMClienId ?? null,
                    CheckAttachment = recordData.CheckAttachment ?? null
                }
                
            };
            var jsonSerializerSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                Formatting = Formatting.None
            };
            string payloadJson = JsonConvert.SerializeObject(payload, jsonSerializerSettings);
            Console.WriteLine( url );
            var content = new StringContent(payloadJson, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await httpClient.PostAsync(url, content);
            Console.WriteLine(payloadJson);
            //Console.WriteLine("status code: " + response.StatusCode);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return await response.Content.ReadAsStringAsync();
            }
            else
            {
                return "no value updated";
            }
        }
    }
}
