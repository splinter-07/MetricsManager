using MetricsAgent;
using MetricsAgent.Controllers;
using MetricsAgent.DAL;
using MetricsAgent.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace MetricsAgentTests
{
    public class DotNetControllerUnitTests
    {
        private DotNetMetricsController _controller;
        private Mock<IDotNetMetricsRepository> _mock;
        private Mock<ILogger<DotNetMetricsController>> _logger;

        public DotNetControllerUnitTests()
        {
            _mock = new Mock<IDotNetMetricsRepository>();
            _logger = new Mock<ILogger<DotNetMetricsController>>();
            _controller = new DotNetMetricsController(_mock.Object, _logger.Object);
        }

        [Fact]
        public void Create_ShouldCall_Create_From_Repository()
        {
            // устанавливаем параметр заглушки
            // в заглушке прописываем что в репозиторий прилетит DotNetMetric объект
            _mock.Setup(repository => repository.Create(It.IsAny<DotNetMetric>())).Verifiable();

            // выполняем действие на контроллере
            var result = _controller.Create(new MetricsAgent.Requests.DotNetMetricCreateRequest { Time = DateTimeOffset.FromUnixTimeSeconds(1), Value = 50 });

            // проверяем заглушку на то, что пока работал контроллер
            // действительно вызвался метод Create репозитория с нужным типом объекта в параметре
            _mock.Verify(repository => repository.Create(It.IsAny<DotNetMetric>()), Times.AtMostOnce());
        }

        [Fact]
        public void All_ShouldCall_All_From_Repository()
        {
            _mock.Setup(repository => repository.GetAll()).Returns(new List<DotNetMetric>());

            var result = _controller.GetAll();

            _mock.Verify(repository => repository.GetAll(), Times.AtMostOnce());
        }

        [Fact]
        public void ByPeriod_ShouldCall_ByPeriod_From_Repository()
        {
            _mock.Setup(repository => repository.GetByTimePeriod(It.IsAny<TimePeriod>())).Returns(new List<DotNetMetric>());

            var result = _controller.GetMetricsDotNetByTimePeriod(new TimePeriod
            {
                From = DateTimeOffset.FromUnixTimeSeconds(1620801171),
                To = DateTimeOffset.FromUnixTimeSeconds(1620801172)
            });

            _mock.Verify(repository => repository.GetByTimePeriod(It.IsAny<TimePeriod>()), Times.AtMostOnce());
        }
    }
}
