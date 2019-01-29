using System.Net.Http;
using System.Threading.Tasks;

namespace NetPointDNS.Http
{
    public interface IClient
    {
        void SetCredentials(Credentials credentials);
        Task<HttpResponseMessage> Post(string url, string resource);
        Task<HttpResponseMessage> Put(string url, string resource);
        Task<HttpResponseMessage> Get(string url);
        Task<HttpResponseMessage> Delete(string url);
    }
}