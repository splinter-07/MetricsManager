using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsManager.Controllers
{
    [Route("api/metrics/network")]
    [ApiController]
    public class NetworkMetricsController : ControllerBase
    {
        private readonly ILogger<NetworkMetricsController> _logger;

        public NetworkMetricsController(ILogger<NetworkMetricsController> logger)
        {
            _logger = logger;
            _logger.LogDebug(1, "NetworkMetricsController");
        }

        [HttpGet("agent")]
        public IActionResult GetMetricsFromAgent([FromQuery] int agentId, [FromBody] TimePeriod period)
        {
            _logger.LogInformation($"GetMetricsFromAgent - agentId {agentId}, from {period.From} - to {period.To}");
            return Ok();
        }

        [HttpGet("cluster")]
        public IActionResult GetMetricsFromAllCluster([FromBody] TimePeriod period)
        {
            _logger.LogInformation($"GetMetricsFromAllCluster - from {period.From} - to {period.To}");
            return Ok();
        }
    }
}
