namespace ADV.BadBroker.WebService.DTO
{
    public class Rate
    {
        public DateTime Date { get; set; }

        public List<СurrencyRate> CurrencySum { get; set; }

        public DateTime BuyDate { get; set; }

        public DateTime SellDate { get; set; }

        public Сurrency Tool { get; set; }

        public Decimal Revenue { get; set; }
    }

    public class СurrencyRate
    {
        public Сurrency Сurrency { get; set; }

        public Decimal Summ { get; set; }
    }
}
