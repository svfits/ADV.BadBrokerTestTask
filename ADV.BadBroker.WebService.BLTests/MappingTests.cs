using Microsoft.VisualStudio.TestTools.UnitTesting;
using ADV.BadBroker.WebService.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace ADV.BadBroker.WebService.BL.Tests
{
    [TestClass()]
    public class MappingTests
    {
        [TestMethod()]
        public void MappingTest()
        {
            var cfg = new MapperConfiguration(expression => expression.AddProfile<Mapping>());
            cfg.AssertConfigurationIsValid();
        }
    }
}