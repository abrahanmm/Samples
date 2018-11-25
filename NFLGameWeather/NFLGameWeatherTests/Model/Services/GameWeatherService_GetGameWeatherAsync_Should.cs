using FluentAssertions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Internal;
using Moq;
using NFLGameWeather.Model;
using NFLGameWeather.Model.Services;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace NFLGameWeatherTests.Model.Services
{
    public class GameWeatherService_GetGameWeatherAsync_Should
    {
        [Fact]
        public async Task ForecastFound_When_TeamKeyIsOk()
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

            mockLogger.Verify(
                m => m.Log(
                    LogLevel.Information,
                    It.IsAny<EventId>(),
                    It.Is<FormattedLogValues>(v => v.ToString().Contains("Found forecast for the game")),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<object, Exception, string>>()
                )
            );

            gameWeather.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void ThrowAnArgumentNullException_When_TeamKeyIsEmpty()
        {
            // Arrange
            var mockForecastService = new Mock<IForecastService>();
            var mockLogger = new Mock<ILogger<GameWeatherService>>();
            GameWeatherService service = new GameWeatherService(mockForecastService.Object, mockLogger.Object);

            // Act and assert
            Func<Task> func = () => service.GetNextGameWeatherAsync(string.Empty);

            // Assert
            func.Should().Throw<ArgumentNullException>().WithMessage("Value cannot be null.\r\nParameter name: teamKey");
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
        public void ThrowAnArgumentException_When_TeamKeyIsWrong()
        {
            // Arrange
            var mockForecastService = new Mock<IForecastService>();
            var mockLogger = new Mock<ILogger<GameWeatherService>>();
            GameWeatherService service = new GameWeatherService(mockForecastService.Object, mockLogger.Object);

            // Act and assert
            Func<Task> func = () => service.GetNextGameWeatherAsync("GV");

            // Assert
            func.Should().Throw<ArgumentException>().WithMessage("The key is not from any team.");

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
