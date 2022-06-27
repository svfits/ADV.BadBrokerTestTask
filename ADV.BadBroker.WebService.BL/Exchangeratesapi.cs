using ADV.BadBroker.DAL;
using ADV.BadBroker.WebService.BL.Exceptions;
using ADV.BadBroker.WebService.BL.ParametrsRate;
using AutoMapper;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace ADV.BadBroker.WebService.BL;

public class Exchangeratesapi : IExchangeratesapi
{
    private readonly HttpClient _httpClient;
    private readonly IMapper _mapper;
    private readonly IOptionsSnapshot<ParametrsRates> _options;

    public Exchangeratesapi(HttpClient httpClient, IMapper mapper, IOptionsSnapshot<ParametrsRates> options)
    {
        _httpClient = httpClient;
        _mapper = mapper;
        _options = options;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="dateOnly"></param>
    /// <returns></returns>
    public async Task<CurrencyReference> GetCurrencyData(DateOnly dateOnly)
    {
        _httpClient.BaseAddress = new Uri(_options.Value.ExchangeratesapiUrl);
        
        var date = string.Concat(dateOnly.Year, "-", dateOnly.Month, "-", dateOnly.Day);

        var type = string.Concat("v1/convert?access_key=", _options.Value.ExchangeratesapiKey, "&base=USD", "&symbols=RUB,EUR,GBP,JPY", "&date=", date);

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

        return _mapper.Map<Rootobject, CurrencyReference>(data);
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
