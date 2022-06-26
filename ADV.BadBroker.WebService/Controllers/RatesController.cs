using ADV.BadBroker.DAL;
using ADV.BadBroker.WebService.BL;
using ADV.BadBroker.WebService.BL.DTO;
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
        private readonly I�alculationService _�alculationService;
        private readonly Context _context;

        public RatesController(ILogger<RatesController> logger, I�alculationService �alculationService, Context context)
        {
            _logger = logger;
            _�alculationService = �alculationService;
            _context = context;
        }

        /// <summary>
        /// Get sales data
        /// </summary>
        /// <param name="startDate">StartDate</param>
        /// <param name="endDate">EndDate</param>
        /// <param name="moneyUsd">MoneyUsd</param>
        /// <returns></returns>
        [HttpGet("/best")]
        public async Task<Rate> GetBest(DateTime startDate, DateTime endDate, Decimal moneyUsd)
        {
            //user needs to be obtained from JWT or from authorization
            var user = _context.Users.First();
            var rate = await _�alculationService.CalculationAsync(user, DateTime.Now, startDate, endDate, moneyUsd);

            return rate;
        }
    }
}