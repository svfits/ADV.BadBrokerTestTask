using ADV.BadBroker.WebService.BL.Exceptions;
using Newtonsoft.Json;

namespace ADV.BadBroker.WebService.BL;

public class Exchangeratesapi : IExchangeratesapi
{
    private readonly HttpClient _httpClient;

    public Exchangeratesapi(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="dateOnly"></param>
    /// <returns></returns>
    public async Task<Rootobject> GetCurrencyData(DateOnly dateOnly)
    {
        var type = string.Concat("/currency_data/convert?base=USD&symbols=RUB,EUR,GBP,JPY&amount=5&date=", dateOnly);
        var request = new HttpRequestMessage(HttpMethod.Get, type);

        var responce = await _httpClient.SendAsync(request);

        if (!responce.IsSuccessStatusCode)
        {
            throw new ExchangeratesapiException("An error occurred while working with Exchangeratesapi");
        }

        var str = await responce.Content.ReadAsStringAsync();

        var data = JsonConvert.DeserializeObject<Rootobject>(str);

        if (data == null)
        {
            throw new ExchangeratesapiException("failed to deserialize object");
        }

        return data;
    }
}


public class Rootobject
{
    [JsonProperty("_base")]
    public string Base { get; set; }

    [JsonProperty("date")]
    public string Date { get; set; }

    [JsonProperty("historical")]
    public bool Historical { get; set; }

    [JsonProperty("rates")]
    public Rates Rates { get; set; }

    [JsonProperty("success")]
    public bool Success { get; set; }

    [JsonProperty("timestamp")]
    public int Timestamp { get; set; }
}

public class Rates
{
    public float RUB { get; set; }
    public float EUR { get; set; }
    public float GBR { get; set; }
    public float JPY { get; set; }
}
