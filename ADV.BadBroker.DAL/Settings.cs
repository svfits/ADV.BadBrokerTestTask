namespace ADV.BadBroker.DAL
{
    public class Settings
    {
        public int Id { get; set; }

        /// <summary>
        /// You have only one chance to buy and sell within a specified period
        /// </summary>
        public TimeSpan SpecifiedPeriod { get; set; } = new TimeSpan(1, 0, 0);
    }
}