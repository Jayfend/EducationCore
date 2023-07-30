using System.Runtime.Serialization;
using Volo.Abp;
using Volo.Abp.ExceptionHandling;
using Volo.Abp.Logging;

namespace IdentityServer.Exceptions
{
    [Serializable]
    public class ExceptionHandler : Exception, IHasErrorCode, IHasErrorDetails, IHasLogLevel, IUserFriendlyException
    {
        public string? Code { get; set; }
        public string? Details { get; set; }

        public LogLevel LogLevel { get; set; }

        public ExceptionHandler(
            string code = null,
            string message = null,
            string details = null,
            Exception innerException = null,
            LogLevel logLevel = LogLevel.Error) : base(message, innerException)
        {
            Code = code;
            Details = details;
            LogLevel = logLevel;
        }

        public ExceptionHandler(SerializationInfo serializationInfo, StreamingContext context) : base(
            serializationInfo, context)
        {
        }

        public ExceptionHandler WithData(string name, object value)
        {
            Data[name] = value;
            return this;
        }
    }
}