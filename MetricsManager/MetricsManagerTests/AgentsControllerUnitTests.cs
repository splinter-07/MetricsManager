using MetricsManager;
using MetricsManager.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using Xunit;
using Moq;

namespace MetricsManagerTests
{
    public class AgentsControllerUnitTests
    {
        private AgentsController _controller;
        private Mock<ILogger<AgentsController>> _logger;
        private Mock<AgentsController> _mock;

        public AgentsControllerUnitTests()
        {
            _mock = new Mock<AgentsController>();
            _logger = new Mock<ILogger<AgentsController>>();
            _controller = new AgentsController(_logger.Object) ;
        }

        [Fact]
        public void RegisterAgent_ReturnsOk()
        {
            //Подготовка данных
            var agentInfo = new AgentInfo();

            //Действие
            var result = _controller.RegisterAgent(agentInfo);

            //Проверка результата
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }

        [Fact]
        public void EnableAgentById_ReturnsOk()
        {
            //Подготовка данных
            var agentId = 1;

            //Действие
            var result = _controller.EnableAgentById(agentId);

            //Проверка результата
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }

        [Fact]
        public void DisableAgentById_ReturnsOk()
        {
            //Подготовка данных
            var agentId = 1;

            //Действие
            var result = _controller.DisableAgentById(agentId);

            //Проверка результата
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }

        [Fact]
        public void GetAllRegisterAgents_ReturnsOk()
        {
            //Действие
            var result = _controller.GetAllRegisterAgents();

            //Проверка результата
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
