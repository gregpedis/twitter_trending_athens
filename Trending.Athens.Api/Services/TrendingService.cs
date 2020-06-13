using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Trending.Athens.Api.Models;
using System.Text.Json;
using System.Globalization;
using Microsoft.Extensions.Logging;
using Trending.Athens.Api.Extensions;

namespace Trending.Athens.Api.Services
{
    public class TrendingService : ITrendingProvider
    {
        private ILogger<TrendingService> _logger;

        public TrendingService(ILogger<TrendingService> logger)
        {
            _logger = logger;
        }

        public List<Trend> GetTrends(DateTime dateCreated)
        {
            var searchTerm = dateCreated.ToString("yyyy_MM_dd") + ".json";

            var datasetDirectory = Path.Combine(Directory.GetCurrentDirectory(), "..", "trending_dataset");
            var filename = Directory.GetFiles(datasetDirectory).FirstOrDefault(fn => fn.Contains(searchTerm));

            if (string.IsNullOrWhiteSpace(filename))
            {
                var errorMessage = dateCreated.Date > DateTime.Now.Date
                    ? "This API does not support time travel."
                    : "No data have been archived for the given date.";

                throw new Exception(errorMessage);
            }

            var json = File.ReadAllText(filename);
            var result = JsonSerializer.Deserialize<List<Trend>>(json);
            return result;
        }

        public List<Trend> GetLastTrends()
        {
            try
            {
                var todayTrends = GetTrends(DateTime.Now);
                return todayTrends;
            }
            catch (Exception e)
            {
                _logger.LogException(e);
                var yesterdayTrends = GetTrends(DateTime.Now.AddDays(-1));
                return yesterdayTrends;
            }
        }

        public List<Date> GetAvailableDateTimes()
        {
            var datasetDirectory = Path.Combine(Directory.GetCurrentDirectory(), "..", "trending_dataset");

            var result = Directory.GetFiles(datasetDirectory)
                .Select(f => Path.GetFileNameWithoutExtension(f))
                .Select(fn => DateTime.ParseExact(fn, "yyyy_MM_dd", CultureInfo.InvariantCulture))
                .Select(dt => new Date(dt.Year, dt.Month, dt.Day))
                .OrderByDescending(d => d.Year).ThenByDescending(d => d.Month).ThenByDescending(d => d.Day)
                .ToList();

            return result;
        }
    }
}
