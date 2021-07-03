using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace LeadApp.Services.HttpClientService.Interfaces
{
    public interface IHttpClientService
    {
        Task<HttpResponseMessage> SendAsync(HttpRequestMessage request);
    }
}
