using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Trending.Athens.Api.Models;
using System.Text.Json;

namespace Trending.Athens.Api.Services
{
    public class TrendingService : ITrendingProvider
    {
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
    }
}
