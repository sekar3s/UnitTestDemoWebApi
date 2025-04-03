// filepath: /Users/sekar3s/Programs/ghcp-demo-apr3/UnitTestDemoWebApi/src/dotnet/csharp/WeatherApi.Tests/Controllers/WeatherForecastsControllerTestsTest.cs
using Xunit;
using WeatherApi.Controllers;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using WeatherApi.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using WeatherApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace WeatherApi.Controllers.Tests
{
    public class WeatherForecastsControllerTestsTest
    {
        [Fact]
        public async Task GetWeatherForecast_ReturnsWeatherForecasts()
        {
            // Setup
            var service = new Mock<IWeatherService>();
            var controller = new WeatherForecastsController(new NullLogger<WeatherForecastsController>(), service.Object);

            var forecasts = new List<WeatherForecast>
            {
                new WeatherForecast { Id = 1, Summary = "Sunny" },
                new WeatherForecast { Id = 2, Summary = "Rainy" }
            };

            service.Setup(x => x.GetWeatherForecastsAsync()).ReturnsAsync(forecasts);

            // Act
            var result = await controller.GetWeatherForecast();

            // Assert
            var actionResult = Assert.IsType<ActionResult<IEnumerable<WeatherForecast>>>(result);
            var returnValue = Assert.IsType<List<WeatherForecast>>(actionResult.Value);
            Assert.Equal(2, returnValue.Count);
        }

        [Fact]
        public async Task GetWeatherForecastById_ReturnsCorrectForecast()
        {
            // Setup
            var service = new Mock<IWeatherService>();
            var controller = new WeatherForecastsController(new NullLogger<WeatherForecastsController>(), service.Object);

            var forecast = new WeatherForecast { Id = 1, Summary = "Sunny" };

            service.Setup(x => x.GetWeatherForecastAsync(1)).ReturnsAsync(forecast);

            // Act
            var result = await controller.GetWeatherForecast(1);

            // Assert
            var actionResult = Assert.IsType<ActionResult<WeatherForecast>>(result);
            var returnValue = Assert.IsType<WeatherForecast>(actionResult.Value);
            Assert.Equal(1, returnValue.Id);
            Assert.Equal("Sunny", returnValue.Summary);
        }

        [Fact]
        public async Task AddWeatherForecast_SuccessfullyAddsForecast()
        {
            // Setup
            var service = new Mock<IWeatherService>();
            var controller = new WeatherForecastsController(new NullLogger<WeatherForecastsController>(), service.Object);

            var newForecast = new WeatherForecast { Id = 3, Summary = "Cloudy" };

            service.Setup(x => x.AddWeatherForecastAsync(It.IsAny<WeatherForecast>())).ReturnsAsync(newForecast);

            // Act
            var result = await controller.AddWeatherForecast(newForecast);

            // Assert
            var actionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var returnValue = Assert.IsType<WeatherForecast>(actionResult.Value);
            Assert.Equal(3, returnValue.Id);
            Assert.Equal("Cloudy", returnValue.Summary);
        }

        [Fact]
        public async Task GetWeatherForecastById_ReturnsNotFoundForInvalidId()
        {
            // Setup
            var service = new Mock<IWeatherService>();
            var controller = new WeatherForecastsController(new NullLogger<WeatherForecastsController>(), service.Object);

            service.Setup(x => x.GetWeatherForecastAsync(It.IsAny<int>())).ReturnsAsync((WeatherForecast)null);

            // Act
            var result = await controller.GetWeatherForecast(999);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task AddWeatherForecast_ReturnsBadRequestForNullInput()
        {
            // Setup
            var service = new Mock<IWeatherService>();
            var controller = new WeatherForecastsController(new NullLogger<WeatherForecastsController>(), service.Object);

            // Act
            var result = await controller.AddWeatherForecast(null);

            // Assert
            Assert.IsType<BadRequestResult>(result.Result);
        }
    }
}