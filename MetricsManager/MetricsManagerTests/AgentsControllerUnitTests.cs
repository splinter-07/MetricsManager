using MetricsManager;
using MetricsManager.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using Xunit;

namespace MetricsManagerTests
{
    public class AgentsControllerUnitTests
    {
        private AgentsController controller;

        public AgentsControllerUnitTests()
        {
            controller = new AgentsController();
        }
        [Fact]
        public void RegisterAgent_ReturnsOk()
        {
            //Подготовка данных
            var agentInfo = new AgentInfo();

            //Действие
            var result = controller.RegisterAgent(agentInfo);

            //Проверка результата
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }

        [Fact]
        public void EnableAgentById_ReturnsOk()
        {
            //Подготовка данных
            var agentId = 1;

            //Действие
            var result = controller.EnableAgentById(agentId);

            //Проверка результата
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }

        [Fact]
        public void DisableAgentById_ReturnsOk()
        {
            //Подготовка данных
            var agentId = 1;

            //Действие
            var result = controller.DisableAgentById(agentId);

            //Проверка результата
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
        [Fact]
        public void GetAllRegisterAgents_ReturnsOk()
        {
            //Действие
            var result = controller.GetAllRegisterAgents();

            //Проверка результата
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
