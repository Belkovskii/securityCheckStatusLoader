using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityCheckStatusLoader.ElmaUseCases
{
    public static class SCS_create_usecase
    {
        async public static Task<string> Create(
            HttpClient httpClient,
            string host,
            ScsUpdateCreateData data
        )
        {
            var url = $"{host}pub/v1/app/_clients/SecurityCheckStatus/create";                                                        
            var payload = new SecurityCheckPayload
            {
                WithEventForceCreate = true,
                context = new ContextObject
                {
                    __directory = "00000000-0000-0000-0000-000000000000",                    
                    CRMClienId = [data.CRMClienId],
                   // StatusId = [data.],
                    CheckDateString = data.CheckDate.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss.fffZ"),
                    CheckAttachmentList = [ new FileAttachment(data.CheckAttachment) ]
                }
            };
            var jsonSerializerSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                Formatting = Formatting.None
            };
            string payloadJson = JsonConvert.SerializeObject(payload, jsonSerializerSettings);
            var content = new StringContent(payloadJson, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await httpClient.PostAsync(url, content);
            Console.WriteLine(response.StatusCode);
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

    public class SecurityCheckPayload
    {
        public ContextObject context { get; set; }

        [JsonProperty("withEventForceCreate")]
        public bool WithEventForceCreate { get; set; }
    }

    public class ContextObject
    {
        public string __directory { get; set; }
        public string __externalId { get; set; } 
        public List<string> CRMClienId { get; set; }
        public List<string> StatusId { get; set; }

        [JsonProperty("CheckAttachment")]
        public List<FileAttachment> CheckAttachmentList { get; set; }

        [JsonProperty("CheckDate")]
        public string CheckDateString { get; set; }
    }
}
