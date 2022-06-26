using System.Runtime.Serialization;

namespace ADV.BadBroker.WebService.BL
{
    [Serializable]
    public class LimitPurchasesException : Exception
    {
        public LimitPurchasesException()
        {
        }

        public LimitPurchasesException(string? message) : base(message)
        {
        }

        public LimitPurchasesException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected LimitPurchasesException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}