using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ACL
{
    internal class ACLClient
    {
        private readonly HttpClient _httpClient;
        public ACLClient(HttpClient client)
        {
            _httpClient = client;
        }

        public ACLClient()
        {
            _httpClient = new(new ACLClientHandler());
        }

        public async Task<string> GetAsync(string requestUrl)
        {
            var res = await _httpClient.GetAsync(requestUrl);
            
            return await res.Content.ReadAsStringAsync();
        }

        public async Task<T> GetAsync<T>(string requestUrl)
        {
            var res = await _httpClient.GetAsync(requestUrl);

            var body = await res.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(body);
        }

        public async Task<T> PostAsync<T>(string requestUrl, object data)
        {
            var res = await _httpClient.PostAsync(requestUrl,
                new StringContent(JsonConvert.SerializeObject(data), MediaTypeHeaderValue.Parse("application/json")));

            var body = await res.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(body);
        }
    }
}
