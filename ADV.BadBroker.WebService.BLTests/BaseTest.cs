using ADV.BadBroker.DAL;

namespace ADV.BadBroker.WebService.BL.Tests
{
    public class BaseTest
    {
        public static List<CurrencyReference> ListCurrencyReference
        {
            get
            {
                return new List<CurrencyReference>(){
                new CurrencyReference() {
                Date = new DateOnly(2014, 12, 15),
                СurrencyValues = new List<СurrencyValue>
                {
                    new СurrencyValue() { Сurrency = Сurrency.RUB, Value = 60.17m},
                    new СurrencyValue() { Сurrency = Сurrency.EUR, Value = 60.17m},
                    new СurrencyValue() { Сurrency = Сurrency.GBR, Value = 60.17m},
                    new СurrencyValue() { Сurrency = Сurrency.JPY, Value = 60.17m},
                    new СurrencyValue() { Сurrency = Сurrency.USD, Value = 60.17m},
                }},
                new CurrencyReference() {
                Date = new DateOnly(2014, 12, 16),
                СurrencyValues = new List<СurrencyValue>
                {
                    new СurrencyValue() { Сurrency = Сurrency.RUB, Value = 72.99m},
                    new СurrencyValue() { Сurrency = Сurrency.EUR, Value = 60.17m},
                    new СurrencyValue() { Сurrency = Сurrency.GBR, Value = 60.17m},
                    new СurrencyValue() { Сurrency = Сurrency.JPY, Value = 60.17m},
                    new СurrencyValue() { Сurrency = Сurrency.USD, Value = 60.17m},
                }},
                new CurrencyReference() {
                Date = new DateOnly(2014, 12, 17),
                СurrencyValues = new List<СurrencyValue>
                {
                    new СurrencyValue() { Сurrency = Сurrency.RUB, Value = 66.01m},
                    new СurrencyValue() { Сurrency = Сurrency.EUR, Value = 60.17m},
                    new СurrencyValue() { Сurrency = Сurrency.GBR, Value = 60.17m},
                    new СurrencyValue() { Сurrency = Сurrency.JPY, Value = 60.17m},
                    new СurrencyValue() { Сurrency = Сurrency.USD, Value = 60.17m},
                }},
                new CurrencyReference() {
                Date = new DateOnly(2014, 12, 18),
                СurrencyValues = new List<СurrencyValue>
                {
                    new СurrencyValue() { Сurrency = Сurrency.RUB, Value = 61.44m},
                    new СurrencyValue() { Сurrency = Сurrency.EUR, Value = 60.17m},
                    new СurrencyValue() { Сurrency = Сurrency.GBR, Value = 60.17m},
                    new СurrencyValue() { Сurrency = Сurrency.JPY, Value = 60.17m},
                    new СurrencyValue() { Сurrency = Сurrency.USD, Value = 60.17m},
                }},
                new CurrencyReference() {
                Date = new DateOnly(2014, 12, 19),
                СurrencyValues = new List<СurrencyValue>
                {
                    new СurrencyValue() { Сurrency = Сurrency.RUB, Value = 59.79m},
                    new СurrencyValue() { Сurrency = Сurrency.EUR, Value = 60.17m},
                    new СurrencyValue() { Сurrency = Сurrency.GBR, Value = 60.17m},
                    new СurrencyValue() { Сurrency = Сurrency.JPY, Value = 60.17m},
                    new СurrencyValue() { Сurrency = Сurrency.USD, Value = 60.17m},
                }},
                new CurrencyReference() {
                Date = new DateOnly(2014, 12, 20),
                СurrencyValues = new List<СurrencyValue>
                {
                    new СurrencyValue() { Сurrency = Сurrency.RUB, Value = 59.79m},
                    new СurrencyValue() { Сurrency = Сurrency.EUR, Value = 60.17m},
                    new СurrencyValue() { Сurrency = Сurrency.GBR, Value = 60.17m},
                    new СurrencyValue() { Сurrency = Сurrency.JPY, Value = 60.17m},
                    new СurrencyValue() { Сurrency = Сurrency.USD, Value = 60.17m},
                }},
                new CurrencyReference() {
                Date = new DateOnly(2014, 12, 21),
                СurrencyValues = new List<СurrencyValue>
                {
                    new СurrencyValue() { Сurrency = Сurrency.RUB, Value = 59.79m},
                    new СurrencyValue() { Сurrency = Сurrency.EUR, Value = 60.17m},
                    new СurrencyValue() { Сurrency = Сurrency.GBR, Value = 60.17m},
                    new СurrencyValue() { Сurrency = Сurrency.JPY, Value = 60.17m},
                    new СurrencyValue() { Сurrency = Сurrency.USD, Value = 60.17m},
                }},
                new CurrencyReference() {
                Date = new DateOnly(2014, 12, 22),
                СurrencyValues = new List<СurrencyValue>
                {
                    new СurrencyValue() { Сurrency = Сurrency.RUB, Value = 54.78m},
                    new СurrencyValue() { Сurrency = Сurrency.EUR, Value = 60.17m},
                    new СurrencyValue() { Сurrency = Сurrency.GBR, Value = 60.17m},
                    new СurrencyValue() { Сurrency = Сurrency.JPY, Value = 60.17m},
                    new СurrencyValue() { Сurrency = Сurrency.USD, Value = 60.17m},
                }},
                new CurrencyReference() {
                Date = new DateOnly(2014, 12, 22),
                СurrencyValues = new List<СurrencyValue>
                {
                    new СurrencyValue() { Сurrency = Сurrency.RUB, Value = 54.80m},
                    new СurrencyValue() { Сurrency = Сurrency.EUR, Value = 60.17m},
                    new СurrencyValue() { Сurrency = Сurrency.GBR, Value = 60.17m},
                    new СurrencyValue() { Сurrency = Сurrency.JPY, Value = 60.17m},
                    new СurrencyValue() { Сurrency = Сurrency.USD, Value = 60.17m},
                }},
            };
            }
        }
    }
}