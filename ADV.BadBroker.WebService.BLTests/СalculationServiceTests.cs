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
    public class СalculationServiceTests
    {
        private Context db;

        [TestInitialize]
        public void Init()
        {
            var options = new DbContextOptionsBuilder<Context>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning))
            .Options;

            db = new Context(options);
        }

        [TestMethod()]
        public async Task Calculation_Revenue()
        {
            ///arrange
            var listRef = new List<CurrencyReference>(){
                new CurrencyReference() {
                Date = new DateOnly(2014, 12, 15),
                СurrencyValues = new List<СurrencyValue>
                {
                    new СurrencyValue() { Сurrency = Сurrency.RUB, Value = 60.17m}
                }},
                new CurrencyReference() {
                Date = new DateOnly(2014, 12, 16),
                СurrencyValues = new List<СurrencyValue>
                {
                    new СurrencyValue() { Сurrency = Сurrency.RUB, Value = 72.99m}
                }},
                new CurrencyReference() {
                Date = new DateOnly(2014, 12, 17),
                СurrencyValues = new List<СurrencyValue>
                {
                    new СurrencyValue() { Сurrency = Сurrency.RUB, Value = 66.01m}
                }},
                new CurrencyReference() {
                Date = new DateOnly(2014, 12, 18),
                СurrencyValues = new List<СurrencyValue>
                {
                    new СurrencyValue() { Сurrency = Сurrency.RUB, Value = 61.44m}
                }},
                new CurrencyReference() {
                Date = new DateOnly(2014, 12, 19),
                СurrencyValues = new List<СurrencyValue>
                {
                    new СurrencyValue() { Сurrency = Сurrency.RUB, Value = 59.79m}
                }},
                new CurrencyReference() {
                Date = new DateOnly(2014, 12, 20),
                СurrencyValues = new List<СurrencyValue>
                {
                    new СurrencyValue() { Сurrency = Сurrency.RUB, Value = 59.79m}
                }},
                new CurrencyReference() {
                Date = new DateOnly(2014, 12, 21),
                СurrencyValues = new List<СurrencyValue>
                {
                    new СurrencyValue() { Сurrency = Сurrency.RUB, Value = 59.79m}
                }},
                new CurrencyReference() {
                Date = new DateOnly(2014, 12, 22),
                СurrencyValues = new List<СurrencyValue>
                {
                    new СurrencyValue() { Сurrency = Сurrency.RUB, Value = 54.78m}
                }},
                new CurrencyReference() {
                Date = new DateOnly(2014, 12, 22),
                СurrencyValues = new List<СurrencyValue>
                {
                    new СurrencyValue() { Сurrency = Сurrency.RUB, Value = 54.80m}
                }},
            };

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
                .ReturnsAsync(new HashSet<CurrencyReference>(listRef));

            exchangeratesapi.Setup(y => y.CheckParam(It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<DateTime>(), user));

            var сalculationService = new СalculationService(logger, exchangeratesapi.Object);
            await сalculationService.CalculationAsync(user, dtNow, startDate, endDate, moneyUsed);

            
        }
    }
}