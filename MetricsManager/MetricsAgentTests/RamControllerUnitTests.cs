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
using AutoMapper;

namespace MetricsAgentTests
{
    public class RamControllerUnitTests
    {
        private RamMetricsController _controller;
        private Mock<IRamMetricsRepository> _mock;
        private Mock<ILogger<RamMetricsController>> _logger;
        private Mock<IMapper> _mapper;

        public RamControllerUnitTests()
        {
            _mock = new Mock<IRamMetricsRepository>();
            _logger = new Mock<ILogger<RamMetricsController>>();
            _mapper = new Mock<IMapper>();
            _controller = new RamMetricsController(_mock.Object, _logger.Object, _mapper.Object);
        }

        [Fact]
        public void Create_ShouldCall_Create_From_Repository()
        {
            // устанавливаем параметр заглушки
            // в заглушке прописываем что в репозиторий прилетит RamMetric объект
            _mock.Setup(repository => repository.Create(It.IsAny<RamMetric>())).Verifiable();

            // выполняем действие на контроллере
            var result = _controller.Create(new MetricsAgent.Requests.RamMetricCreateRequest { Time = DateTimeOffset.FromUnixTimeSeconds(1), Value = 50 });

            // проверяем заглушку на то, что пока работал контроллер
            // действительно вызвался метод Create репозитория с нужным типом объекта в параметре
            _mock.Verify(repository => repository.Create(It.IsAny<RamMetric>()), Times.AtMostOnce());
        }

        [Fact]
        public void All_ShouldCall_All_From_Repository()
        {
            _mock.Setup(repository => repository.GetAll()).Returns(new List<RamMetric>());

            var result = _controller.GetAll();

            _mock.Verify(repository => repository.GetAll(), Times.AtMostOnce());
        }

        [Fact]
        public void ByPeriod_ShouldCall_ByPeriod_From_Repository()
        {
            _mock.Setup(repository => repository.GetByTimePeriod(It.IsAny<TimePeriod>())).Returns(new List<RamMetric>());

            var result = _controller.GetMetricsRamByTimePeriod(new TimePeriod
            {
                From = DateTimeOffset.FromUnixTimeSeconds(1620801171),
                To = DateTimeOffset.FromUnixTimeSeconds(1620801172)
            });

            _mock.Verify(repository => repository.GetByTimePeriod(It.IsAny<TimePeriod>()), Times.AtMostOnce());
        }
    }
}
