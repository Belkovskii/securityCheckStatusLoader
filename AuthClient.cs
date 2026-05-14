using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SecurityCheckStatusLoader
{

    public class AuthClient(string _loginToken, string _host)
    {
        private readonly string loginToken = _loginToken;

        private readonly string host = _host;        

        public async Task<string> Auth()
        {
            string authEndoint = host + "api/auth";
            Uri baseAddress = new(authEndoint);
            var cookieContainer = new CookieContainer();
            using var handler = new HttpClientHandler() { CookieContainer = cookieContainer };            
            using var client = new HttpClient(handler) { BaseAddress = baseAddress };            
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", loginToken);
            HttpResponseMessage response = await client.GetAsync(baseAddress);                       
            string resultText = await response.Content.ReadAsStringAsync();
            HttpHeaders headers = response.Headers;                      
            return response.StatusCode.ToString();
        }
    }
}
