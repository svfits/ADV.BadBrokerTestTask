using ADV.BadBroker.DAL;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADV.BadBroker.WebService.BL
{
    public class СalculationService
    {
        private readonly ILogger<СalculationService> _logger;
        private readonly Context _context;
        private readonly Exchangeratesapi _exchangeratesapi;
        private readonly IMapper _mapper;

        public СalculationService(Context context, Exchangeratesapi exchangeratesapi, ILogger<СalculationService> logger, IMapper mapper)
        {
            _logger = logger;
            _context = context;
            _exchangeratesapi = exchangeratesapi;
            _mapper = mapper;
        }

        public async Task Calculation(User user, DateTime dtNow, DateTime startDate, DateTime endDate, Decimal moneyUsd)
        {
            _logger.LogInformation("Start Calculation for user { user }", user);

            var specifiedPeriod = _context.Settings.First().SpecifiedPeriod;
            var dateLastpurchase = _context.UserExtradition.First(h => h.User.Id == user.Id).PaymentDate;

            if ((dtNow - dateLastpurchase) < specifiedPeriod)
            {
                throw new LimitPurchases("Limit on the number of purchases");
            }

            if (startDate.AddDays(60) < endDate)
            {
                throw new IntervalDate("Interval no more than 60");
            }

            var startDateOnly = DateOnly.FromDateTime(startDate);
            var endDateOnly = DateOnly.FromDateTime(endDate);

            var days = new HashSet<DateOnly>(capacity: 60)
            {
                startDateOnly,
            };

            for (int i = 0; i <= 60; i++)
            {
                days.Add(DateOnly.FromDateTime(startDate.AddDays(i)));
            }

            var hasAlready = _context.CurrencyReference
                .Where(d => d.Date > startDateOnly && d.Date < endDateOnly)
                ;

            var lacksCourses = days.Except(hasAlready.Select(h => h.Date));

            var coursesReceived = new HashSet<CurrencyReference>(capacity: 60);
            foreach (var day in lacksCourses)
            {
                var coursesExchangeratesapi = await _exchangeratesapi.GetCurrencyData(day);

                var courceDb = _mapper.Map<Rootobject, CurrencyReference>(coursesExchangeratesapi);
                coursesReceived.Add(courceDb);

                await _context.CurrencyReference.AddAsync(courceDb);
            }
            
            await _context.SaveChangesAsync();

            coursesReceived.Union(hasAlready);

        }
    }
}
