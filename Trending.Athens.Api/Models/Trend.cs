using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Trending.Athens.Api.Models
{
    public class Trend
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("volume")]
        public int? Volume { get; set; }
    }
}
