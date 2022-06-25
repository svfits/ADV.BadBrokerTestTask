using ADV.BadBroker.WebService.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace ADV.BadBroker.WebService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    public class RatesController : ControllerBase
    {
        private readonly ILogger<RatesController> _logger;

        public RatesController(ILogger<RatesController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Get sales data
        /// </summary>
        /// <param name="startDate">StartDate</param>
        /// <param name="endDate">EndDate</param>
        /// <param name="moneyUsd">MoneyUsd</param>
        /// <returns></returns>
        [HttpGet("/best")]
        public Rate GetBest(DateTime startDate, DateTime endDate, Decimal moneyUsd)
        {
            var rr = new Rate() 
            {
                BuyDate = startDate,
                SellDate = endDate,
                Tool = �urrency.RUB.ToString(),
                Revenue = moneyUsd,
                CurrencySum = new [] { new �urrencyRate() 
                {
                    Date = startDate,
                    EUR = moneyUsd,
                    GBR = moneyUsd,
                    JPY = moneyUsd,
                    RUB = moneyUsd,
                }}
            };

            return rr;
        }
    }
}