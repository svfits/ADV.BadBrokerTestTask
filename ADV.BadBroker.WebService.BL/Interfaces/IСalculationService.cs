using ADV.BadBroker.DAL;

namespace ADV.BadBroker.WebService.BL
{
    public interface IСalculationService
    {
        Task CalculationAsync(User user, DateTime dtNow, DateTime startDate, DateTime endDate, decimal moneyUsd);
    }
}