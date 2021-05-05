using MetricsAgent.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using Xunit;

namespace MetricsAgentTests
{
    public class RamControllerUnitTests
    {
        private RamMetricsController controller;

        public RamControllerUnitTests()
        {
            controller = new RamMetricsController();
        }

        [Fact]
        public void GetMetricsRamFreeRamSize_ResultOK()
        {
            //Подготовка данных
            var fromTime = DateTimeOffset.FromUnixTimeSeconds(0);
            var toTime = DateTimeOffset.FromUnixTimeSeconds(100);

            //Действие
            var result = controller.GetMetricsRamFreeRamSize(fromTime, toTime);

            //Проверка результата
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
