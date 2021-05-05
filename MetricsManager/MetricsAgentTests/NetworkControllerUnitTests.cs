using MetricsAgent.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using Xunit;

namespace MetricsAgentTests
{
    public class NetworkControllerUnitTests
    {
        private NetworkMetricsController controller;

        public NetworkControllerUnitTests()
        {
            controller = new NetworkMetricsController();
        }
        [Fact]
        public void GetMetricsNetworkFromTime_ReturnsOk()
        {
            //Подготовка данных
            var fromTime = DateTimeOffset.FromUnixTimeSeconds(0);
            var toTime = DateTimeOffset.FromUnixTimeSeconds(100);

            //Действие
            var result = controller.GetMetricsNetworkFromTime(fromTime, toTime);

            //Проверка результата
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
