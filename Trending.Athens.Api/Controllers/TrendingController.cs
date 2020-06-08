using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Trending.Athens.Api.Services;
using Trending.Athens.Api.Models;
using Trending.Athens.Api.Extensions;

namespace Trending.Athens.Api.Controllers
{
    [ApiController]
    [Route("api/trending")]
    [Produces("application/json")]
    public class TrendingController : ControllerBase
    {
        private readonly ILogger<TrendingController> _logger;
        private readonly ITrendingProvider _trendingProvider;

        /// <summary>
        /// Executes any necessary start up code for the controller.
        /// </summary>
        public TrendingController(ILogger<TrendingController> logger, ITrendingProvider trendingProvider)
        {
            _logger = logger;
            _trendingProvider = trendingProvider;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<Trend>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetTrendingByDate(DateTime dateCreated)
        {
            try
            {
                var result = _trendingProvider.GetTrends(dateCreated);
                return this.Ok(result);
            }
            catch (Exception e)
            {
                _logger.LogException(e);
                return BadRequest("An unexpected error occured.");
            }
        }
    }
}