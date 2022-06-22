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
                   CurrencySum = new List<�urrencyRate>()
                   {
                       new �urrencyRate() { Summ = 45.54m, �urrency = �urrency.RUB},
                       new �urrencyRate() { Summ = 45.54m, �urrency = �urrency.GBR},
                       new �urrencyRate() { Summ = 45.54m, �urrency = �urrency.EUR},
                       new �urrencyRate() { Summ = 45.54m, �urrency = �urrency.JPY},
                   },
                   Date = DateTime.Now,
                   Revenue = 34.67M,
                   SellDate = DateTime.Now,
                   Tool = �urrency.RUB,
               }
            };
        }
    }
}