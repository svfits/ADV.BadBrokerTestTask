using ADV.BadBroker.DAL;
using ADV.BadBroker.WebService.BL;
using ADV.BadBroker.WebService.BL.DTO;
using ADV.BadBroker.WebService.BL.Exceptions;
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
        private readonly IÑalculationService _ñalculationService;
        private readonly Context _context;

        public RatesController(ILogger<RatesController> logger, IÑalculationService ñalculationService, Context context)
        {
            _logger = logger;
            _ñalculationService = ñalculationService;
            _context = context;
        }

        /// <summary>
        /// Get sales data
        /// </summary>
        /// <param name="startDate">startDate</param>
        /// <param name="endDate">endDate</param>
        /// <param name="moneyUsd">moneyUsd</param>
        /// <returns></returns>
        /// <response code="200">Returns rate</response>
        /// <response code="400">Invalid request parameters</response>
        /// <response code="503">The service for receiving exchange rates is not working</response>
        [HttpGet("/best")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        public async Task<ActionResult<Rate>> GetBest(DateTime startDate, DateTime endDate, Decimal moneyUsd)
        {
            //user needs to be obtained from JWT or from authorization
            var user = _context.Users.First();
            try
            {
                var rate = await _ñalculationService.CalculationAsync(user, DateTime.Now, startDate, endDate, moneyUsd);
                return rate;
            }
            catch (ExchangeratesapiException)
            {
                return StatusCode(503);
            }
            catch(IntervalDateException)
            {
                return StatusCode(400);
            }
            catch(LimitPurchasesException)
            {
                return StatusCode(401);
            }
        }
    }
}