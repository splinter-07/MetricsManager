using AutoMapper;
using MetricsAgent.DAL;
using MetricsAgent.Models;
using MetricsAgent.Requests;
using MetricsAgent.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/ram")]
    [ApiController]
    public class RamMetricsController : ControllerBase
    {
        private IRamMetricsRepository _repository;
        private readonly ILogger<RamMetricsController> _logger;
        private readonly IMapper _mapper;

        public RamMetricsController(IRamMetricsRepository repository, ILogger<RamMetricsController> logger, IMapper mapper)
        {
            this._repository = repository;
            _logger = logger;
            _logger.LogDebug(1, "NetworkMetricsController");
            this._mapper = mapper;
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] RamMetricCreateRequest request)
        {
            _repository.Create(new RamMetric
            {
                Time = request.Time,
                Value = request.Value
            });

            _logger.LogInformation($"Create - Value: {request.Value}, Time: {request.Time} - Entry added successfully");

            return Ok();
        }

        [HttpGet("all")]
        public IActionResult GetAll()
        {
            var metrics = _repository.GetAll();

            var response = new AllRamMetricsResponse()
            {
                Metrics = new List<RamMetricDto>()
            };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(_mapper.Map<RamMetricDto>(metric));
            }

            _logger.LogInformation($"All records were successfully displayed");

            return Ok(response);
        }

        [HttpGet("byperiod")]
        public IActionResult GetMetricsRamByTimePeriod([FromBody] TimePeriod timePeriod)
        {
            var metrics = _repository.GetByTimePeriod(timePeriod);

            var response = new AllRamMetricsResponse()
            {
                Metrics = new List<RamMetricDto>()
            };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(_mapper.Map<RamMetricDto>(metric));
            }

            _logger.LogInformation($"All records of the period: {timePeriod.From} - {timePeriod.To} displayed successfully");

            return Ok(response);
        }
    }
}
