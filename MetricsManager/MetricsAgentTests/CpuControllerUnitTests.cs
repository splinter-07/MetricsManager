using MetricsAgent.Controllers;
using MetricsAgent.DAL;
using MetricsAgent.Models;
using Moq;
using Microsoft.AspNetCore.Mvc;
using System;
using Xunit;
using Microsoft.Extensions.Logging;
using MetricsAgent;
using System.Collections.Generic;

namespace MetricsAgentTests
{
    public class CpuControllerUnitTests
    {
        private CpuMetricsController _controller;
        private Mock<ICpuMetricsRepository> _mock;
        private Mock<ILogger<CpuMetricsController>> _logger;

        public CpuControllerUnitTests()
        {
            _mock = new Mock<ICpuMetricsRepository>();
            _logger = new Mock<ILogger<CpuMetricsController>>();
            _controller = new CpuMetricsController(_mock.Object, _logger.Object);
        }

        [Fact]
        public void Create_ShouldCall_Create_From_Repository()
        {
            // устанавливаем параметр заглушки
            // в заглушке прописываем что в репозиторий прилетит CpuMetric объект
            _mock.Setup(repository => repository.Create(It.IsAny<CpuMetric>())).Verifiable();

            // выполняем действие на контроллере
            var result = _controller.Create(new MetricsAgent.Requests.CpuMetricCreateRequest { Time = DateTimeOffset.FromUnixTimeSeconds(1), Value = 50 });

            // проверяем заглушку на то, что пока работал контроллер
            // действительно вызвался метод Create репозитория с нужным типом объекта в параметре
            _mock.Verify(repository => repository.Create(It.IsAny<CpuMetric>()), Times.AtMostOnce());
        }

        [Fact]
        public void All_ShouldCall_All_From_Repository()
        {
            _mock.Setup(repository => repository.GetAll()).Returns(new List<CpuMetric>());

            var result = _controller.GetAll();

            _mock.Verify(repository => repository.GetAll(), Times.AtMostOnce());
        }

        [Fact]
        public void ByPeriod_ShouldCall_ByPeriod_From_Repository()
        {
            _mock.Setup(repository => repository.GetByTimePeriod(It.IsAny<TimePeriod>())).Returns(new List<CpuMetric>());

            var result = _controller.GetMetricsCpuByTimePeriod(new TimePeriod
                {
                    From = DateTimeOffset.FromUnixTimeSeconds(1620801171),
                    To = DateTimeOffset.FromUnixTimeSeconds(1620801172)
                });

            _mock.Verify(repository => repository.GetByTimePeriod(It.IsAny<TimePeriod>()), Times.AtMostOnce());
        }
    }
}
