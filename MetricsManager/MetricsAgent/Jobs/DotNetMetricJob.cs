using MetricsAgent.DAL;
using Quartz;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsAgent.Jobs
{
    [DisallowConcurrentExecution]
    public class DotNetMetricJob : IJob
    {
        private IDotNetMetricsRepository _repository;
        private PerformanceCounter _dotnetCounter;

        public DotNetMetricJob(IDotNetMetricsRepository repository)
        {
            _repository = repository;
            _dotnetCounter = new PerformanceCounter("Process", "% User Time", "_Total");
        }

        public Task Execute(IJobExecutionContext context)
        {
            var dotnetUsageInPercents = Convert.ToInt32(_dotnetCounter.NextValue());

            var time = DateTimeOffset.UtcNow;

            _repository.Create(new Models.DotNetMetric { Time = time, Value = dotnetUsageInPercents });

            return Task.CompletedTask;
        }
    }
}
