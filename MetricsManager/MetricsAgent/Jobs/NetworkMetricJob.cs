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
    public class NetworkMetricJob : IJob
    {
        private INetworkMetricsRepository _repository;
        private PerformanceCounter _networkCounter;

        public NetworkMetricJob(INetworkMetricsRepository repository)
        {
            _repository = repository;
            _networkCounter = new PerformanceCounter("System", "Threads");
        }

        public Task Execute(IJobExecutionContext context)
        {
            var networkUsageInPercents = Convert.ToInt32(_networkCounter.NextValue());

            var time = DateTimeOffset.UtcNow;

            _repository.Create(new Models.NetworkMetric { Time = time, Value = networkUsageInPercents });

            return Task.CompletedTask;
        }
    }
}
