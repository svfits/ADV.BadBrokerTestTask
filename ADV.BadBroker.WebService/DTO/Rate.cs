using Newtonsoft.Json;

namespace ADV.BadBroker.WebService.DTO;

public class Rate
{
    [JsonProperty("ratesgggggggggggggggggg")]
    public СurrencyRate[] CurrencySum { get; set; }

    [JsonProperty("buyDategdfggggggggggggggggg")]
    public DateTime BuyDate { get; set; }

    [JsonProperty("sellDate")]
    public DateTime SellDate { get; set; }

    [JsonProperty("tool")]
    public string Tool { get; set; }

    [JsonProperty("revenue")]
    public Decimal Revenue { get; set; }
}

public class СurrencyRate
{
    public DateTime Date { get; set; }

    [JsonProperty("rub")]
    public Decimal RUB { get; set; }

    [JsonProperty("eur")]
    public Decimal EUR { get; set; }

    [JsonProperty("gbr")]
    public Decimal GBR { get; set; }

    [JsonProperty("jpy")]
    public Decimal JPY { get; set; }
}
