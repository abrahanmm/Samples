﻿using NFLGameWeather.Model;
using NFLGameWeather.Model.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace NFLGameWeatherTests.Model.Services
{
    public class FirstVersionServiceTests
    {
        [Fact]
        public async Task GetGameWeatherAsync_HappyWay()
        {
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

            FirstVersionService service = new FirstVersionService();
            GameWeather gameWeather = await service.GetGameWeatherAsync("GB");

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
    }
}
