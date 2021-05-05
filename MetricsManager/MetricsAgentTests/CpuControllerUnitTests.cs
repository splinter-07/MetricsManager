using MetricsAgent.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using Xunit;

namespace MetricsAgentTests
{
    public class CpuControllerUnitTests
    {
        private CpuMetricsController controller;

        public CpuControllerUnitTests()
        {
            controller = new CpuMetricsController();
        }

        [Fact]
        public void GetMetricsCpuFromTime_ReturnsOk()
        {
            //Подготовка данных
            var fromTime = DateTimeOffset.FromUnixTimeSeconds(0);
            var toTime = DateTimeOffset.FromUnixTimeSeconds(100);

            //Действие
            var result = controller.GetMetricsCpuFromTime(fromTime, toTime);

            //Проверка результата
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
