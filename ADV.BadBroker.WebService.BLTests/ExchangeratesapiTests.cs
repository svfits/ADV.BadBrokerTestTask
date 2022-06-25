using ADV.BadBroker.DAL;
using ADV.BadBroker.WebService.BL.Exceptions;
using AutoFixture;
using AutoMapper;
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
        private Mapper mapper;
        private HttpClient httpClient;

        [TestInitialize]
        public void Init()
        {
            mapper = new Mapper(new MapperConfiguration(c => { c.AddProfile<Mapping>(); }));

            var mockMessageHandler = new Mock<HttpMessageHandler>();
            mockMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.InternalServerError
                });

            httpClient = new HttpClient(mockMessageHandler.Object) { BaseAddress = new Uri("http://localhost/") };
        }

        [TestMethod()]
        [ExpectedException(typeof(ExchangeratesapiException))]
        public async Task GetCurrencyDataTest_InternalServerError()
        {
            //arrange
            var exchangeratesapi = new Exchangeratesapi(httpClient, mapper);

            var dt = new DateOnly(2022, 6, 23);

            //action
            await exchangeratesapi.GetCurrencyData(dt);
        }

        [TestMethod()]
        [ExpectedException(typeof(ExchangeratesapiException))]
        public async Task GetCurrencyDataTest()
        {
            //arrange
            var exchangeratesapi = new Exchangeratesapi(httpClient, mapper);

            var dt = new DateOnly(2022, 6, 23);

            //action
            await exchangeratesapi.GetCurrencyData(dt);
        }

        [TestMethod()]
        public async Task GetCurrencyDataTest_Ok()
        {
            //arrange
            var dt = new DateOnly(2022, 6, 23);
            var fixture = new Fixture();
            var rootobject = fixture.Create<Rootobject>();
            rootobject.Date = dt.ToString();

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

            var exchangeratesapi = new Exchangeratesapi(httpClient, mapper);

            //action
            var output = await exchangeratesapi.GetCurrencyData(dt);

            var ex = mapper.Map<CurrencyReference, Rootobject>(output);

            Assert.IsNotNull(output);
            Assert.AreEqual(ex.Rates.JPY, rootobject.Rates.JPY);
            Assert.AreEqual(ex.Rates.RUB, rootobject.Rates.RUB);
            Assert.AreEqual(ex.Rates.EUR, rootobject.Rates.EUR);
            Assert.AreEqual(ex.Rates.GBR, rootobject.Rates.GBR);
        }
    }
}