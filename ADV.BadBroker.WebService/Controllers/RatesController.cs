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
        public async Task<Decimal> GetBest(DateTime startDate, DateTime endDate, Decimal moneyUsd)
        {
            return 34.567M;
        }
    }
}