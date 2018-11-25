using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Internal;
using Moq;
using NFLGameWeather.Model;
using NFLGameWeather.Model.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace NFLGameWeatherTests.Model.Services
{
    public class GameWeatherServiceTest
    {
        [Fact]
        public async Task GetGameWeatherAsync_TeamKeyIsOk_ForecastFound()
        {
            // Arrange
            Game expectedGame = Game.Schedule
               .Where(x => x.HomeTeam.Equals(Team.Packers) || x.AwayTeam.Equals(Team.Packers))
               .Where(x => x.Date.Date > DateTime.UtcNow.AddDays(-1).Date)
               .FirstOrDefault();

            GameWeather expected = new GameWeather(
                expectedGame,
                new Forecast()
                {
                    Minimum = "9,3° C",
                    Maximum = "12,5° C",
                    Day = "Showers",
                    Night = "Mostly cloudy"
                }
            );

            var mockForecastService = new Mock<IForecastService>();
            mockForecastService.Setup(x => x.GetForecastAsync(It.IsAny<float>(), It.IsAny<float>(), It.IsAny<DateTime>())).Returns(Task.FromResult<Forecast>(expected.Forecast));
            var mockLogger = new Mock<ILogger<GameWeatherService>>();

            GameWeatherService service = new GameWeatherService(mockForecastService.Object, mockLogger.Object);

            // Act
            GameWeather gameWeather = await service.GetNextGameWeatherAsync(Team.Packers.Key);

            // Assert
            mockForecastService.Verify(m => m.GetForecastAsync(It.IsAny<float>(), It.IsAny<float>(), It.IsAny<DateTime>()), Times.Once);

            // The commented line don't work because LogInformation is a extension method.
            // mockLogger.Verify(m => m.LogInformation(It.IsAny<string>()), Times.Once);
            mockLogger.Verify(
                m => m.Log(
                    LogLevel.Information,
                    It.IsAny<EventId>(),
                    It.Is<FormattedLogValues>(v => v.ToString().Contains("Found forecast for the game")),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<object, Exception, string>>()
                )
            );

            Assert.Equal(expected.HomeTeam, gameWeather.HomeTeam);
            Assert.Equal(expected.AwayTeam, gameWeather.AwayTeam);
            Assert.Equal(expected.Stadium, gameWeather.Stadium);
            Assert.Equal(expected.Date, gameWeather.Date);
            Assert.Equal(expected.Forecast.Day, gameWeather.Forecast.Day);
            Assert.Equal(expected.Forecast.Night, gameWeather.Forecast.Night);
            Assert.Equal(expected.Forecast.Maximum, gameWeather.Forecast.Maximum);
            Assert.Equal(expected.Forecast.Minimum, gameWeather.Forecast.Minimum);
            Assert.Equal(expected.City, gameWeather.City);

            // The next line don't work because expected and gameWeather are not the same object (reference) despite having the same properties.
            //Assert.Equal(expected, gameWeather);
        }

        [Fact]
        public async Task GetGameWeatherAsync_TeamKeyIsEmpty_ArgumentNullException()
        {
            // Arrange
            var mockForecastService = new Mock<IForecastService>();
            var mockLogger = new Mock<ILogger<GameWeatherService>>();
            GameWeatherService service = new GameWeatherService(mockForecastService.Object, mockLogger.Object);

            // Act and assert
            ArgumentNullException exception = await Assert.ThrowsAsync<ArgumentNullException>(() => service.GetNextGameWeatherAsync(string.Empty));

            // Assert
            Assert.Equal("Value cannot be null.\r\nParameter name: teamKey", exception.Message);
            mockLogger.Verify(
                m => m.Log(
                    LogLevel.Warning,
                    It.IsAny<EventId>(),
                    It.Is<FormattedLogValues>(v => v.ToString().Equals("Empty key")),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<object, Exception, string>>()
                )
            );
        }

        [Fact]
        public async Task GetGameWeatherAsync_TeamKeyIsWrong_ArgumentException()
        {
            // Arrange
            var mockForecastService = new Mock<IForecastService>();
            var mockLogger = new Mock<ILogger<GameWeatherService>>();
            GameWeatherService service = new GameWeatherService(mockForecastService.Object, mockLogger.Object);

            // Act and assert
            ArgumentException exception = await Assert.ThrowsAsync<ArgumentException>(() => service.GetNextGameWeatherAsync("GV"));

            // Assert
            Assert.Equal("The key is not from any team.", exception.Message);
            mockLogger.Verify(
                m => m.Log(
                    LogLevel.Warning,
                    It.IsAny<EventId>(),
                    It.Is<FormattedLogValues>(v => v.ToString().Equals("The key GV is not from any team.")),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<object, Exception, string>>()
                )
            );
        }
    }
}
