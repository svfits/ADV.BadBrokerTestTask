using ADV.BadBroker.DAL;

namespace ADV.BadBroker.WebService.BL
{
    public interface IСalculationServiceHelper
    {
        void CheckParam(DateTime dtNow, DateTime startDate, DateTime endDate, User user);
        Task<HashSet<CurrencyReference>> GetDataAsync(DateTime startDate, DateTime endDate);
    }
}