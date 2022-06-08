using Newtonsoft.Json;
using ParkyWeb.Repository.Interfaces;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ParkyWeb.Repository.Implementations
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly IHttpClientFactory _client;
        public Repository(IHttpClientFactory client)
        {
            _client = client;
        }
        public async Task<bool> Create(string url, T createDto, string token = null)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, url);
            if (createDto != null)
                request.Content = new StringContent(JsonConvert.SerializeObject(createDto), Encoding.UTF8, "application/json");
            else
                return false;

            var client = _client.CreateClient();
            if (token != null)
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await client.SendAsync(request);
            if (response.StatusCode == HttpStatusCode.Created)
                return true;
            else
                return false;
        }

        public async Task<bool> Delete(string url, int id, string token = null)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, url + id);

            var client = _client.CreateClient();
            var response = await client.SendAsync(request);
            if (response.StatusCode == HttpStatusCode.NoContent)
                return true;
            else
                return false;
        }

        public async Task<IEnumerable<T>> GetAll(string url, string token = null)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, url);

            var client = _client.CreateClient();
            if (token != null)
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await client.SendAsync(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IEnumerable<T>>(jsonString);
            }
            return null;
        }

        public async Task<T> GetById(string url, int id, string token = null)
        {

            var request = new HttpRequestMessage(HttpMethod.Get, url + id);

            var client = _client.CreateClient();
            if (token != null)
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await client.SendAsync(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(jsonString);
            }
            return null;
        }

        public async Task<bool> Update(string url, T updateDto, string token = null)
        {
            var request = new HttpRequestMessage(HttpMethod.Patch, url);
            if (updateDto != null)
                request.Content = new StringContent(JsonConvert.SerializeObject(updateDto), Encoding.UTF8, "application/json");
            else
                return false;

            var client = _client.CreateClient();
            if (token != null)
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await client.SendAsync(request);
            if (response.StatusCode == HttpStatusCode.NoContent)
                return true;
            else
                return false;
        }
    }
}
