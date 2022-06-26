﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
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
using AutoFixture;

namespace ADV.BadBroker.WebService.BL.Tests
{
    [TestClass()]
    public class СalculationServiceHelperTests : BaseTest
    {
        private Context db;
        private ILogger<СalculationServiceHelper> logger;
        private Fixture fixture;

        [TestInitialize()]
        public void Init()
        {
            var options = new DbContextOptionsBuilder<Context>()
         .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
         .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning))
         .Options;

            db = new Context(options);

            logger = Mock.Of<ILogger<СalculationServiceHelper>>();

            fixture = new Fixture();
        }

        [TestMethod()]
        [ExpectedException(typeof(LimitPurchasesException))]
        public async Task CheckParamTest_LimitPurchasesException()
        {
            //arrange
            var dtNow = new DateTime(2014, 12, 16);
            var startDate = new DateTime(2014, 12, 16);
            var endDate = new DateTime(2014, 12, 16);

            var user = fixture.Create<User>();

            var exchangeratesapiMock = new Mock<IExchangeratesapi>();

            var сalculationServiceHelper = new СalculationServiceHelper(logger, db, exchangeratesapiMock.Object);

            await db.Settings.AddAsync(new Settings() { SpecifiedPeriod = new TimeSpan(1, 0, 0, 0) });
            await db.Users.AddAsync(user);
            await db.UserExtradition.AddAsync(new UserExtradition() { User = user, PaymentDate = dtNow.AddHours(-3) });
            await db.SaveChangesAsync();

            //action
            сalculationServiceHelper.CheckParam(dtNow, startDate, endDate, user);
        }

        [TestMethod()]
        [ExpectedException(typeof(IntervalDateException))]
        public async Task CheckParamTest_IntervalDateException()
        {
            //arrange
            var dtNow = new DateTime(2014, 12, 16);
            var startDate = new DateTime(2014, 12, 16);
            var endDate = startDate.AddDays(61);

            var user = fixture.Create<User>();

            var exchangeratesapiMock = new Mock<IExchangeratesapi>();

            var сalculationServiceHelper = new СalculationServiceHelper(logger, db, exchangeratesapiMock.Object);

            await db.Settings.AddAsync(new Settings() { LimitHistoricalPeriod = new TimeSpan(60, 0, 0, 0) });
            await db.Users.AddAsync(user);
            await db.UserExtradition.AddAsync(new UserExtradition() { User = user, PaymentDate = dtNow.AddDays(-3) });
            await db.SaveChangesAsync();

            //action
            сalculationServiceHelper.CheckParam(dtNow, startDate, endDate, user);
        }
    }
}