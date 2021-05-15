using MetricsManager;
using MetricsManager.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using Xunit;

namespace MetricsManagerTests
{
    public class CpuControllerUnitTests
    {
        private CpuMetricsController _controller;
        private Mock<ILogger<CpuMetricsController>> _logger;
        private Mock<CpuMetricsController> _mock;


        public CpuControllerUnitTests()
        {
            _mock = new Mock<CpuMetricsController>();
            _logger = new Mock<ILogger<CpuMetricsController>>();
            _controller = new CpuMetricsController(_logger.Object);
        }
        [Fact]
        public void GetMetricsFromAgent_ReturnsOk()
        {
            //���������� ������
            var agentId = 1;
            var period = new TimePeriod
            {
                From = DateTimeOffset.FromUnixTimeSeconds(0),
                To = DateTimeOffset.FromUnixTimeSeconds(100)
            };

            //��������
            var result = _controller.GetMetricsFromAgent(agentId, period);

            //�������� ����������
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }

        [Fact]
        public void GetMetricsFromAllCluster_ReturnsOk()
        {
            //���������� ������
            var period = new TimePeriod
            {
                From = DateTimeOffset.FromUnixTimeSeconds(0),
                To = DateTimeOffset.FromUnixTimeSeconds(100)
            };

            //��������
            var result = _controller.GetMetricsFromAllCluster(period);

            //�������� ����������
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
