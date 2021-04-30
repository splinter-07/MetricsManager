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
            //���������� ������
            var fromTime = TimeSpan.FromSeconds(0);
            var toTime = TimeSpan.FromSeconds(100);

            //��������
            var result = controller.GetMetricsCpuFromTime(fromTime, toTime);

            //�������� ����������
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
