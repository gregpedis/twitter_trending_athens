using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trending.Athens.Api.Extensions
{
    public static class ILoggerExtensions
    {
        /// <summary>
        /// Fine-grained exception logging.
        /// </summary>
        public static void LogException<T>(this ILogger<T> logger, Exception e)
        {
            var exception = new StringBuilder()
                .Append(e.Message).Append(e.StackTrace).Append(e.InnerException)
                .ToString();

            logger.LogError(exception);
        }
    }
}