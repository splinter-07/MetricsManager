using MetricsManager;
using MetricsManager.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using Xunit;

namespace MetricsManagerTests
{
    public class RamControllerUnitTests
    {
        private RamMetricsController _controller;
        private Mock<ILogger<RamMetricsController>> _logger;
        private Mock<RamMetricsController> _mock;

        public RamControllerUnitTests()
        {
            _mock = new Mock<RamMetricsController>();
            _logger = new Mock<ILogger<RamMetricsController>>();
            _controller = new RamMetricsController(_logger.Object);
        }

        [Fact]
        public void GetMetricsFromAgent_ReturnsOk()
        {
            //Подготовка данных
            var agentId = 1;
            var period = new TimePeriod
                                        {
                                            From = DateTimeOffset.FromUnixTimeSeconds(0),
                                            To = DateTimeOffset.FromUnixTimeSeconds(100)
                                        };

            //Действие
            var result = _controller.GetMetricsFromAgent(agentId, period);

            //Проверка результата
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }

        [Fact]
        public void GetMetricsFromAllCluster_ReturnsOk()
        {
            //Подготовка данных
            var period = new TimePeriod
                                        {
                                            From = DateTimeOffset.FromUnixTimeSeconds(0),
                                            To = DateTimeOffset.FromUnixTimeSeconds(100)
                                        };

            //Действие
            var result = _controller.GetMetricsFromAllCluster(period);

            //Проверка результата
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
