using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;


namespace MetricsManager.Controllers
{
    [Route("api/metrics/cpu")]
    [ApiController]
    public class CpuMetricsController : ControllerBase
    {
        private readonly ILogger<CpuMetricsController> _logger;

        public CpuMetricsController(ILogger<CpuMetricsController> logger)
        {
            _logger = logger;
            _logger.LogDebug(1, "CpuMetricsController");
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
