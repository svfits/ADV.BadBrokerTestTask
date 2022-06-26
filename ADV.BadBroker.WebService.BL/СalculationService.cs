using ADV.BadBroker.DAL;
using ADV.BadBroker.WebService.BL.DTO;
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace ADV.BadBroker.WebService.BL;

public class СalculationService : IСalculationService
{
    private readonly ILogger<СalculationService> _logger;
    private readonly IСalculationServiceHelper _сalculationServiceHelper;
    private readonly IMapper _mapper;
    private const int daysBbrokerFee = 1;

    public СalculationService(ILogger<СalculationService> logger, IСalculationServiceHelper сalculationServiceHelper, IMapper mapper)
    {
        _logger = logger;
        _сalculationServiceHelper = сalculationServiceHelper;
        _mapper = mapper;
    }

    public async Task<Rate> CalculationAsync(User user, DateTime dtNow, DateTime startDate, DateTime endDate, Decimal moneyUsd)
    {
        _logger.LogInformation("Start Calculation for user { user }", user);

        _сalculationServiceHelper.CheckParam(dtNow, startDate, endDate, user);

        HashSet<CurrencyReference> coursesReceived = await _сalculationServiceHelper.GetDataAsync(startDate, endDate);

        var maxCurrency = coursesReceived
            .SelectMany(g => g.СurrencyValues)
            .Where(g => g.Сurrency == DAL.Сurrency.RUB)
            .Max(d => d.Value)
            ;

        var minCurrency = coursesReceived
            .SelectMany(g => g.СurrencyValues)
            .Where(g => g.Сurrency == DAL.Сurrency.RUB)
            .Min(d => d.Value)
            ;

        var rates = _mapper.Map<HashSet<CurrencyReference>, DTO.Rates[]>(coursesReceived);

        decimal totalDays = (decimal)(endDate - startDate).TotalDays;
        var revenue = ((maxCurrency * moneyUsd / minCurrency) - totalDays * daysBbrokerFee) - moneyUsd;

        var rate = new Rate()
        {
            Tool = DAL.Сurrency.RUB.ToString(),
            Revenue = revenue,
            BuyDate = startDate,
            SellDate = endDate,
            Rates = rates
        };

        return rate;
    }
}

public class СalculationServiceHelper : IСalculationServiceHelper
{
    private readonly ILogger<СalculationServiceHelper> _logger;
    private readonly Context _context;
    private readonly IExchangeratesapi _exchangeratesapi;

    public СalculationServiceHelper(ILogger<СalculationServiceHelper> logger, Context context, IExchangeratesapi exchangeratesapi)
    {
        _logger = logger;
        _context = context;
        _exchangeratesapi = exchangeratesapi;
    }

    public void CheckParam(DateTime dtNow, DateTime startDate, DateTime endDate, User user)
    {
        var limit = _context.Settings.First();
        var dateLastpurchase = _context.UserExtradition.First(h => h.User.Id == user.Id).PaymentDate;

        if ((dtNow - dateLastpurchase) < limit.SpecifiedPeriod)
        {
            _logger.LogError("Limit on the number of purchases");
            throw new LimitPurchasesException("Limit on the number of purchases");
        }

        if ((endDate - startDate) > limit.LimitHistoricalPeriod)
        {
            _logger.LogError("Interval no more than {limit.LimitHistoricalPeriod}", limit.LimitHistoricalPeriod);
            throw new IntervalDateException($"Interval no more than {limit.LimitHistoricalPeriod}");
        }
    }

    /// <summary>
    /// getting data from cache or database
    /// </summary>
    /// <param name="startDate"></param>
    /// <param name="endDate"></param>
    /// <returns>all exchange rates for the specified range</returns>
    public async Task<HashSet<CurrencyReference>> GetDataAsync(DateTime startDate, DateTime endDate)
    {
        var startDateOnly = DateOnly.FromDateTime(startDate);
        var endDateOnly = DateOnly.FromDateTime(endDate);

        int countDays = (int)(endDate - startDate).TotalDays;

        var days = new HashSet<DateOnly>(capacity: 60)
        {
            startDateOnly,
        };

        for (int i = 0; i < countDays; i++)
        {
            var dd = DateOnly.FromDateTime(startDate.AddDays(i));
            days.Add(dd);
        }

        var hasAlready = _context.CurrencyReference
            .Where(d => d.Date >= startDateOnly && d.Date <= endDateOnly)
            .ToHashSet();
            ;

        var lacksCourses = days.Except(hasAlready.Select(h => h.Date));
                
        foreach (var day in lacksCourses)
        {
            var coursesExchangeratesapi = await _exchangeratesapi.GetCurrencyData(day);

            hasAlready.Add(coursesExchangeratesapi);

            await _context.CurrencyReference.AddAsync(coursesExchangeratesapi);
        }

        await _context.SaveChangesAsync();

        return hasAlready;
    }
}
