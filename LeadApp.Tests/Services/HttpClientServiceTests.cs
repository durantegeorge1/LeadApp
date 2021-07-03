using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using LeadApp.Services.HttpClientService;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.Protected;

namespace LeadApp.Tests.Services
{
    [TestClass]
    public class HttpClientServiceTests
    {
        [TestMethod]
        public async Task SendAsync_ShouldSendHttpRequestAndReturnHttpResponseMessage()
        {
            //arrange
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent("{'LastName' : 'Doe'}"),
                });
            Mock<HttpClient> mockHttpClient = new(MockBehavior.Strict, mockHttpMessageHandler.Object);
            Mock<IHttpClientFactory> mockHttpClientFactory = new();

            mockHttpClientFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(mockHttpClient.Object);
            HttpClientService sut = new(mockHttpClientFactory.Object);
            HttpRequestMessage requestMessage = new(HttpMethod.Get, "https://localhost/");

            //act
            HttpResponseMessage result = await sut.SendAsync(requestMessage);

            //assert
            Assert.IsInstanceOfType(result, typeof(HttpResponseMessage));
            string json = await result.Content.ReadAsStringAsync();
            Assert.AreEqual("{'LastName' : 'Doe'}", json);
            mockHttpClientFactory.Verify(hcf => hcf.CreateClient(It.IsAny<string>()), Times.Once());
        }
    }
}
