using System.Runtime.Serialization;

namespace ADV.BadBroker.WebService.BL
{
    [Serializable]
    public class IntervalDateException : Exception
    {
        public IntervalDateException()
        {
        }

        public IntervalDateException(string? message) : base(message)
        {
        }

        public IntervalDateException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected IntervalDateException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}