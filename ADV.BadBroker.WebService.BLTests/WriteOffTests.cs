using Microsoft.VisualStudio.TestTools.UnitTesting;
using ADV.BadBroker.WebService.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ADV.BadBroker.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using AutoFixture;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace ADV.BadBroker.WebService.BL.Tests
{
    [TestClass()]
    public class WriteOffTests
    {
        private Context db;
        private ILogger<WriteOff> logger;
        private DateTime dtNow;
        private const Decimal totalSum = 1000;

        [TestInitialize]
        public async Task Init()
        {
            var options = new DbContextOptionsBuilder<Context>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning))
            .Options;

            db = new Context(options);

            dtNow = new DateTime(2022, 6, 23);

            var fixture = new Fixture();

            var scoreRub = fixture.Create<SettlementAccount>();
            scoreRub.Start = dtNow.AddDays(-2);
            scoreRub.Currency = Сurrency.RUB;
            await db.SettlementAccount.AddAsync(scoreRub);
            await db.SaveChangesAsync();

            var scoreUSD = fixture.Create<SettlementAccount>();
            scoreUSD.Currency = Сurrency.USD;
            scoreUSD.TotalCurrency = totalSum;            

            scoreUSD.User = await db.Users.FirstAsync(a => a.Id == scoreRub.User.Id);

            await db.SettlementAccount.AddAsync(scoreUSD);
            await db.SaveChangesAsync();

            logger = Mock.Of<ILogger<WriteOff>>();
        }

        [TestMethod()]
        public async Task AccrualTest()
        {
            //arrange
            var writeOff = new WriteOff(db, logger);

            //action
            await writeOff.Accrual(dtNow);

            //assert
            var outputTotal = await db.SettlementAccount
                .FirstAsync(a => a.Currency == Сurrency.USD)
                ;
            var expected = totalSum - 1;
            Assert.AreEqual(expected, outputTotal.TotalCurrency);
        }
    }
}