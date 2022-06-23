using ADV.BadBroker.WebService.BL.Exceptions;
using AutoFixture;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using System.Net;

namespace ADV.BadBroker.WebService.BL.Tests
{
    [TestClass()]
    public class ExchangeratesapiTests
    {
        [TestMethod()]
        [ExpectedException(typeof(ExchangeratesapiException))]
        public async Task GetCurrencyDataTest_InternalServerError()
        {
            //arrange
            var mockMessageHandler = new Mock<HttpMessageHandler>();
            mockMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.InternalServerError
                });

            var httpClient = new HttpClient(mockMessageHandler.Object) { BaseAddress = new Uri("http://localhost/") };

            var exchangeratesapi = new Exchangeratesapi(httpClient);

            var dt = new DateOnly(2022, 6, 23);

            //action
            await exchangeratesapi.GetCurrencyData(dt);
        }

        [TestMethod()]
        [ExpectedException(typeof(ExchangeratesapiException))]
        public async Task GetCurrencyDataTest()
        {
            //arrange
            var mockMessageHandler = new Mock<HttpMessageHandler>();
            mockMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK
                });

            var httpClient = new HttpClient(mockMessageHandler.Object) { BaseAddress = new Uri("http://localhost/") };

            var exchangeratesapi = new Exchangeratesapi(httpClient);

            var dt = new DateOnly(2022, 6, 23);

            //action
            await exchangeratesapi.GetCurrencyData(dt);
        }

        [TestMethod()]
        public async Task GetCurrencyDataTest_Ok()
        {
            //arrange
            var fixture = new Fixture();
            var rootobject = fixture.Create<Rootobject>();

            var stringContent = JsonConvert.SerializeObject(rootobject);

            var mockMessageHandler = new Mock<HttpMessageHandler>();
            mockMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(stringContent)
                });

            var httpClient = new HttpClient(mockMessageHandler.Object) { BaseAddress = new Uri("http://localhost/") };

            var exchangeratesapi = new Exchangeratesapi(httpClient);

            var dt = new DateOnly(2022, 6, 23);

            //action
            var output = await exchangeratesapi.GetCurrencyData(dt);

            Assert.IsNotNull(output);
            output.Should().BeEquivalentTo(rootobject);
        }
    }
}