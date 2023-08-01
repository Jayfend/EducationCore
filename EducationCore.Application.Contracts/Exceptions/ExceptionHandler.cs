using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.ExceptionHandling;
using Volo.Abp.Logging;

namespace EducationCore.Application.Contracts.Exceptions
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