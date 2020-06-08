using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trending.Athens.Api.Models;

namespace Trending.Athens.Api.Services
{
    public interface ITrendingProvider
    {
        public List<Trend> GetTrends(DateTime dateCreated);

    }
}
