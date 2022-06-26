using Newtonsoft.Json;

namespace ADV.BadBroker.WebService.BL.DTO;

public class Rate
{
    [JsonProperty("rates")]
    public Rates[] Rates { get; set; }

    [JsonProperty("buyDate")]
    public DateTime BuyDate { get; set; }

    [JsonProperty("sellDate")]
    public DateTime SellDate { get; set; }

    [JsonProperty("tool")]
    public string Tool { get; set; }

    [JsonProperty("revenue")]
    public Decimal Revenue { get; set; }
}

[JsonArray("rates")]
public class Rates
{
    [JsonProperty("date")]
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
