
namespace ADV.BadBroker.WebService.BL
{
    public interface IExchangeratesapi
    {
        Task<Rootobject> GetCurrencyData(DateOnly dateOnly);
    }
}