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
    public class HddMetricJob : IJob
    {
        private IHddMetricsRepository _repository;
        private PerformanceCounter _hddCounter;

        public HddMetricJob(IHddMetricsRepository repository)
        {
            _repository = repository;
            _hddCounter = new PerformanceCounter("Cache", "Dirty Pages");
        }

        public Task Execute(IJobExecutionContext context)
        {
            var hddUsageInPercents = Convert.ToInt32(_hddCounter.NextValue());

            var time = DateTimeOffset.UtcNow;

            _repository.Create(new Models.HddMetric { Time = time, Value = hddUsageInPercents });

            return Task.CompletedTask;
        }
    }
}
