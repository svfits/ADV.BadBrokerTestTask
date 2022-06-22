using ADV.BadBroker.WebService.DTO;
using Microsoft.AspNetCore.Mvc;

namespace ADV.BadBroker.WebService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RatesController : ControllerBase
    {
        private readonly ILogger<RatesController> _logger;

        public RatesController(ILogger<RatesController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "best")]
        public IEnumerable<Rate> Get()
        {
            return new List<Rate>()
            {
               new Rate()
               {
                   BuyDate = DateTime.Now,
                   CurrencySum = new List<ÑurrencyRate>()
                   {
                       new ÑurrencyRate() { Summ = 45.54m, Ñurrency = Ñurrency.RUB},
                       new ÑurrencyRate() { Summ = 45.54m, Ñurrency = Ñurrency.GBR},
                       new ÑurrencyRate() { Summ = 45.54m, Ñurrency = Ñurrency.EUR},
                       new ÑurrencyRate() { Summ = 45.54m, Ñurrency = Ñurrency.JPY},
                   },
                   Date = DateTime.Now,
                   Revenue = 34.67M,
                   SellDate = DateTime.Now,
                   Tool = Ñurrency.RUB,
               }
            };
        }
    }
}