using MetricsAgent.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using Xunit;

namespace MetricsAgentTests
{
    public class DotNetControllerUnitTests
    {
        private DotNetMetricsController controller;

        public DotNetControllerUnitTests()
        {
            controller = new DotNetMetricsController();
        }
        [Fact]
        public void GetMetricsDotNetFromTime_ReturnsOk()
        {
            //Подготовка данных
            var fromTime = DateTimeOffset.FromUnixTimeSeconds(0);
            var toTime = DateTimeOffset.FromUnixTimeSeconds(100);

            //Действие
            var result = controller.GetMetricsDotNetFromTime(fromTime, toTime);

            //Проверка результата
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
