using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityCheckStatusLoader.ElmaUseCases
{
    public static class SCS_getById_usecase
    {
        public static async Task<string> SCS_getByIdAsync(Guid id, HttpClient client)
        {
            string url = $"https://l42bom5pymlbs.elma365.ru/pub/v1/app/_clients/SecurityCheckStatus/{id}/get";
            HttpResponseMessage response = await client.GetAsync(url);
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
