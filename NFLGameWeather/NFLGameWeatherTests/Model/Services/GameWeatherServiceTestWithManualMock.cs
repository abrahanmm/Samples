using Microsoft.Extensions.Logging;
using NFLGameWeather.Model;
using NFLGameWeather.Model.Services;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace NFLGameWeatherTests.Model.Services
{
    public class GameWeatherServiceTestWithManualMock
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

            var mockForecastService = new MockForecastService(expected.Forecast);
            var mockLogger = new MockLogger();

            GameWeatherService service = new GameWeatherService(mockForecastService, mockLogger);

            // Act
            GameWeather gameWeather = await service.GetNextGameWeatherAsync(Team.Packers.Key);

            // Assert
            Assert.Equal(expected.HomeTeam, gameWeather.HomeTeam);
            Assert.Equal(expected.AwayTeam, gameWeather.AwayTeam);
            Assert.Equal(expected.Stadium, gameWeather.Stadium);
            Assert.Equal(expected.Date, gameWeather.Date);
            Assert.Equal(expected.Forecast.Day, gameWeather.Forecast.Day);
            Assert.Equal(expected.Forecast.Night, gameWeather.Forecast.Night);
            Assert.Equal(expected.Forecast.Maximum, gameWeather.Forecast.Maximum);
            Assert.Equal(expected.Forecast.Minimum, gameWeather.Forecast.Minimum);
            Assert.Equal(expected.City, gameWeather.City);

            //Assert.Equal(expected, gameWeather);
        }

        [Fact]
        public async Task GetGameWeatherAsync_TeamKeyIsEmpty_ArgumentNullException()
        {
            // Arrange
            var mockForecastService = new MockForecastService(null);
            var mockLogger = new MockLogger();
            GameWeatherService service = new GameWeatherService(mockForecastService, mockLogger);

            // Act and assert
            ArgumentNullException exception = await Assert.ThrowsAsync<ArgumentNullException>(() => service.GetNextGameWeatherAsync(string.Empty));

            // Assert
            Assert.Equal("Value cannot be null.\r\nParameter name: teamKey", exception.Message);
        }

        [Fact]
        public async Task GetGameWeatherAsync_TeamKeyIsWrong_ArgumentException()
        {
            // Arrange
            var mockForecastService = new MockForecastService(null);
            var mockLogger = new MockLogger();
            GameWeatherService service = new GameWeatherService(mockForecastService, mockLogger);

            // Act and assert
            ArgumentException exception = await Assert.ThrowsAsync<ArgumentException>(() => service.GetNextGameWeatherAsync("GV"));

            // Assert
            Assert.Equal("The key is not from any team.", exception.Message);
        }
    }

    public class MockForecastService : IForecastService
    {
        public Forecast Forecast { get; }

        public MockForecastService(Forecast forecast)
        {
            this.Forecast = forecast;
        }

        public Task<Forecast> GetForecastAsync(float geoLatitude, float geoLongitude, DateTime date)
        {
            return Task.FromResult(this.Forecast);
        }
    }

    public class MockLogger : ILogger<GameWeatherService>
    {
        public IDisposable BeginScope<TState>(TState state)
        {
            throw new NotImplementedException();
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
        }
    }
}

