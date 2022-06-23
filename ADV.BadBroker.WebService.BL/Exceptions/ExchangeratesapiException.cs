using System.Runtime.Serialization;

namespace ADV.BadBroker.WebService.BL.Exceptions;

[Serializable]
public class ExchangeratesapiException : Exception
{
    public ExchangeratesapiException()
    {
    }

    public ExchangeratesapiException(string? message) : base(message)
    {
    }

    public ExchangeratesapiException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected ExchangeratesapiException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}
