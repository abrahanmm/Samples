using NFLGameWeather.Model;
using NFLGameWeather.Model.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace NFLGameWeatherTests.Model.Services
{
    public class FirstVersionServiceTests
    {
        [Fact]
        public async Task GetGameWeatherAsync_TeamKeyIsOk_ForecastFound()
        {
            // Arrange
            Game expectedGame = Game.Schedule
               .Where(x => x.HomeTeam.Equals(Team.Texans) || x.AwayTeam.Equals(Team.Texans))
               .Where(x => x.Date.Date > DateTime.UtcNow.AddDays(-1).Date)
               .FirstOrDefault();

            GameWeather expected = new GameWeather(
                expectedGame,
                new Forecast()
                {
                    Minimum = "5,1° C",
                    Maximum = "22,6° C",
                    Day = "Mostly cloudy",
                    Night = "Partly cloudy"
                }
            );

            FirstVersionService service = new FirstVersionService();

            // Act
            GameWeather gameWeather = await service.GetNextGameWeatherAsync(Team.Texans.Key);

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
            FirstVersionService service = new FirstVersionService();
            ArgumentNullException exception = await Assert.ThrowsAsync<ArgumentNullException>(() => service.GetNextGameWeatherAsync(string.Empty));
            Assert.Equal("Value cannot be null.\r\nParameter name: teamKey", exception.Message);
        }

        [Fact]
        public async Task GetGameWeatherAsync_TeamKeyIsWrong_ArgumentException()
        {
            FirstVersionService service = new FirstVersionService();
            ArgumentException exception = await Assert.ThrowsAsync<ArgumentException>(() => service.GetNextGameWeatherAsync("GV"));
            Assert.Equal("The key is not from any team.", exception.Message);
        }
    }
}
