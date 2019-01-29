using System;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;

namespace NetPointDNS.Http
{
    public class Client : IClient
    {
        private const string MediaType = "application/json";
        private Credentials _credentials;

        public Client(Credentials credentials)
        {
            _credentials = credentials;
        }

        public Client() { }

        public void SetCredentials(Credentials credentials)
        {
            _credentials = credentials;
        }

        public async Task<HttpResponseMessage> Post(string url, string resource)
        {
            using (HttpClient client = new HttpClient())
            {
                var clientPayload =
                    new StringContent(resource, Encoding.UTF8, MediaType);

                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("basic", _credentials.Encode());
                client.BaseAddress = new Uri(url);

                return await client.PostAsync(client.BaseAddress, clientPayload);
            }
        }

        public async Task<HttpResponseMessage> Put(string url, string resource)
        {
            using (HttpClient client = new HttpClient())
            {
                var clientPayload =
                    new StringContent(JsonConvert.SerializeObject(resource), Encoding.UTF8, MediaType);

                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("basic", _credentials.Encode());
                client.BaseAddress = new Uri(url);

                return await client.PutAsync(client.BaseAddress, clientPayload);
            }
        }

        public async Task<HttpResponseMessage> Get(string url)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("basic", _credentials.Encode());
                client.BaseAddress = new Uri(url);

                return await client.GetAsync(client.BaseAddress);
            }
        }

        public async Task<HttpResponseMessage> Delete(string url)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("basic", _credentials.Encode());
                client.BaseAddress = new Uri(url);

                return await client.DeleteAsync(client.BaseAddress);
            }
        }
    }
}
