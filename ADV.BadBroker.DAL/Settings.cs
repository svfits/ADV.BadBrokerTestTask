namespace ADV.BadBroker.DAL
{
    public class Settings
    {
        /// <summary>
        /// You have only one chance to buy and sell within a specified period
        /// </summary>
        public TimeSpan SpecifiedPeriod { get; set; }
    }
}