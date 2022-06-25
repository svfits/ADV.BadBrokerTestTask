
using ADV.BadBroker.DAL;

namespace ADV.BadBroker.WebService.BL
{
    public interface IExchangeratesapi
    {
        Task<CurrencyReference> GetCurrencyData(DateOnly dateOnly);
    }
}