using Microsoft.VisualStudio.TestTools.UnitTesting;
using ADV.BadBroker.WebService.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ADV.BadBroker.DAL;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Moq;
using Microsoft.Extensions.Logging;
using AutoMapper;
using AutoFixture;

namespace ADV.BadBroker.WebService.BL.Tests
{
    [TestClass()]
    public class СalculationServiceTests : BaseTest
    {
        private Context db;
        private Mapper mapper;

        [TestInitialize]
        public void Init()
        {
            var options = new DbContextOptionsBuilder<Context>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning))
            .Options;

            db = new Context(options);

            mapper = new Mapper(new MapperConfiguration(c => { c.AddProfile<Mapping>(); }));
        }

        [TestMethod()]
        public async Task Calculation_Revenue()
        {
            ///arrange
            var fixture = new Fixture();
            var user = fixture.Create<User>();

            var dtNow = new DateTime(2014, 12, 23);
            var startDate = new DateTime(2014, 12, 16);
            var endDate = new DateTime(2014, 12, 22);
            var moneyUsed = 100;
            //await db.CurrencyReference.AddRangeAsync(listRef);
            //await db.SaveChangesAsync();

            var logger = Mock.Of<ILogger<СalculationService>>();

            var exchangeratesapi = new Mock<IСalculationServiceHelper>();
            exchangeratesapi.Setup(y => y.GetDataAsync(It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                .ReturnsAsync(new HashSet<CurrencyReference>(ListCurrencyReference));

            exchangeratesapi.Setup(y => y.CheckParam(It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<DateTime>(), user));

            var сalculationService = new СalculationService(logger, exchangeratesapi.Object, mapper);

            //action
            var output = await сalculationService.CalculationAsync(user, dtNow, startDate, endDate, moneyUsed);

            //assert
            Assert.IsNotNull(output);
            Assert.AreEqual(endDate, output.SellDate);
            Assert.AreEqual(startDate, output.BuyDate);
            Assert.AreEqual("RUB", output.Tool);
            decimal revenue = 27.258783297622983m;
            decimal delta = 0.05m;
            Assert.AreEqual(revenue, output.Revenue, delta);
        }
    }
}