using System.Runtime.Serialization;

namespace ADV.BadBroker.WebService.BL
{
    [Serializable]
    internal class LimitPurchases : Exception
    {
        public LimitPurchases()
        {
        }

        public LimitPurchases(string? message) : base(message)
        {
        }

        public LimitPurchases(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected LimitPurchases(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}