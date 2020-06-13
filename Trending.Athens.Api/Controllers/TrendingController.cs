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

        [HttpGet("data/{year:int}/{month:int}/{day:int}")]
        [ProducesResponseType(typeof(List<Trend>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetTrendingByDate([FromRoute]int year, [FromRoute]int month, [FromRoute] int day)
        {
            try
            {
                var dateCreated = new DateTime(year, month, day);
                var result = _trendingProvider.GetTrends(dateCreated);
                return this.Ok(result);
            }
            catch (Exception e)
            {
                _logger.LogException(e);
                return BadRequest("An unexpected error occured.");
            }
        }

        [HttpGet("data/last")]
        [ProducesResponseType(typeof(List<Trend>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetLastTrending()
        {
            try
            {
                var result = _trendingProvider.GetLastTrends();
                return this.Ok(result);
            }
            catch (Exception e)
            {
                _logger.LogException(e);
                return BadRequest("An unexpected error occured.");
            }
        }

        [HttpGet("dates")]
        [ProducesResponseType(typeof(List<Date>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetAvailableDates()
        {
            try
            {
                var result = _trendingProvider.GetAvailableDateTimes();
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
