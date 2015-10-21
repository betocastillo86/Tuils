using System;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Logging;
using System.Text;

namespace Nop.Services.Logging
{
    public static class LoggingExtensions
    {
        public static void Debug(this ILogger logger, string message, Exception exception = null, Customer customer = null)
        {
            FilteredLog(logger, LogLevel.Debug, message, exception, customer);
        }

        public static void Debug(this ILogger logger, object reflectObject, Exception exception = null, Customer customer = null)
        {
            FilteredLog(logger, LogLevel.Debug, ToStringObject(reflectObject), exception, customer);
        }

        public static void Information(this ILogger logger, string message, Exception exception = null, Customer customer = null)
        {
            FilteredLog(logger, LogLevel.Information, message, exception, customer);
        }

        public static void Information(this ILogger logger, object reflectObject, Exception exception = null, Customer customer = null)
        {
            FilteredLog(logger, LogLevel.Information, ToStringObject(reflectObject), exception, customer);
        }

        private static string ToStringObject(object reflectObject)
        {
            var str = new StringBuilder();
            foreach (var prop in reflectObject.GetType().GetProperties())
            {
                str.AppendFormat("{0} = {1}\n", prop.Name, prop.GetValue(reflectObject));
            }
            return str.ToString();
        }


        public static void Warning(this ILogger logger, string message, Exception exception = null, Customer customer = null)
        {
            FilteredLog(logger, LogLevel.Warning, message, exception, customer);
        }
        public static void Error(this ILogger logger, string message, Exception exception = null, Customer customer = null)
        {
            FilteredLog(logger, LogLevel.Error, message, exception, customer);
        }
        public static void Fatal(this ILogger logger, string message, Exception exception = null, Customer customer = null)
        {
            FilteredLog(logger, LogLevel.Fatal, message, exception, customer);
        }

        private static void FilteredLog(ILogger logger, LogLevel level, string message, Exception exception = null, Customer customer = null)
        {
            //don't log thread abort exception
            if (exception is System.Threading.ThreadAbortException)
                return;

            if (logger.IsEnabled(level))
            {
                string fullMessage = exception == null ? string.Empty : exception.ToString();
                logger.InsertLog(level, message, fullMessage, customer);
            }
        }
    }
}
