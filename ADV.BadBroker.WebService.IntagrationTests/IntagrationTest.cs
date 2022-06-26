using ADV.BadBroker.DAL;
using ADV.BadBroker.WebService.BL;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System.Net;

namespace ADV.BadBroker.WebService.IntagrationTests
{
    [TestClass]
    public class IntagrationTest
    {
        private string dataBaseName;
        private HttpClient TestClient;

        [TestInitialize]
        public void Init()
        {
            var ExchangeratesapiMock = new Mock<Exchangeratesapi>();

            var appFactory = new WebApplicationFactory<Program>()
               .WithWebHostBuilder(builder =>
               {
                   builder.ConfigureServices(services =>
                   {
                       var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<Context>));
                       services.Remove(descriptor);
                       dataBaseName = Guid.NewGuid().ToString();

                       services.AddDbContext<Context>(options => options.UseInMemoryDatabase(databaseName: dataBaseName));

                       var sp = services.BuildServiceProvider();

                       using var scope = sp.CreateScope();
                       var scopedServices = scope.ServiceProvider;
                       var db = scopedServices.GetRequiredService<Context>();

                       db.Database.EnsureCreated();

                       services.AddTransient(_ => ExchangeratesapiMock.Object);
                   });
               });

            TestClient = appFactory.CreateClient();
        }

        [TestMethod]
        public async Task Intagration()
        {
            //arrange
            var startdate = "2014-12-16T15%3A23%3A23.730Z";
            var endDate = "2014-12-22T15%3A23%3A23.730Z";
            var moneyUsd = 100;

            var url = string.Concat("best/?startDate=", startdate, "&endDate=", endDate, "&moneyUsd=", moneyUsd);

            //action
            //https://localhost:7126/best?startDate=2022-06-26T15%3A23%3A23.730Z&endDate=2022-06-26T15%3A23%3A23.730Z&moneyUsd=333'
            var result = await TestClient.GetAsync(url);

            //Assert
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
        }
    }
}