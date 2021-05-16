using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SQLite;
using MetricsAgent.DAL;
using MetricsAgent.Requests;
using MetricsAgent.Responses;
using MetricsAgent.Models;
using Microsoft.Extensions.Logging;
using AutoMapper;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/cpu")]
    [ApiController]
    public class CpuMetricsController : ControllerBase
    {
        private ICpuMetricsRepository _repository;
        private readonly ILogger<CpuMetricsController> _logger;
        private readonly IMapper _mapper;

        public CpuMetricsController(ICpuMetricsRepository repository, ILogger<CpuMetricsController> logger, IMapper mapper)
        {
            this._repository = repository;
            _logger = logger;
            _logger.LogDebug(1, "CpuMetricsController");
            this._mapper = mapper;
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] CpuMetricCreateRequest request)
        {
            _repository.Create(new CpuMetric
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

            var response = new AllCpuMetricsResponse()
            {
                Metrics = new List<CpuMetricDto>()
            };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(_mapper.Map<CpuMetricDto>(metric));
            }

            _logger.LogInformation($"All records were successfully displayed");

            return Ok(response);
        }
    
        [HttpGet("byperiod")]
        public IActionResult GetMetricsCpuByTimePeriod([FromBody] TimePeriod timePeriod)
        {
            var metrics = _repository.GetByTimePeriod(timePeriod);

            var response = new AllCpuMetricsResponse()
            {
                Metrics = new List<CpuMetricDto>()
            };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(_mapper.Map<CpuMetricDto>(metric));
            }

            _logger.LogInformation($"All records of the period: {timePeriod.From} - {timePeriod.To} displayed successfully");
            
            return Ok(response);
        }  
    }
}
