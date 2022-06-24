using System.Runtime.Serialization;

namespace ADV.BadBroker.WebService.BL
{
    [Serializable]
    internal class IntervalDate : Exception
    {
        public IntervalDate()
        {
        }

        public IntervalDate(string? message) : base(message)
        {
        }

        public IntervalDate(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected IntervalDate(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}