using System;
using System.Net.Http;
using System.Threading.Tasks;
using LeadApp.Services.HttpClientService.Interfaces;

namespace LeadApp.Services.HttpClientService
{
    public class HttpClientService : IHttpClientService
    {
        private readonly IHttpClientFactory httpClientFactory;

        public HttpClientService(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        public async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request)
        {
            HttpClient client = httpClientFactory.CreateClient();
            return await client.SendAsync(request);
        }
    }
}
