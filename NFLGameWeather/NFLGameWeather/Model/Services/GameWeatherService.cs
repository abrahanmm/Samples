using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace NFLGameWeather.Model.Services
{
    public class GameWeatherService : IGameWeatherService
    {
        private IForecastService ForecastService;

        public GameWeatherService(IForecastService forecastService)
        {
            this.ForecastService = forecastService;
        }

        public async Task<GameWeather> GetNextGameWeatherAsync(string teamKey)
        {
            if (string.IsNullOrWhiteSpace(teamKey))
                throw new ArgumentNullException(nameof(teamKey));

            Team team = Team.GetTeams().Where(x => x.Key.Equals(teamKey.ToUpper())).FirstOrDefault();

            if (team is null)
                throw new ArgumentException("The key is not from any team.");

            Game game = Game.Schedule
                .Where(x => x.HomeTeam.Equals(team) || x.AwayTeam.Equals(team))
                .Where(x => x.Date.Date > DateTime.UtcNow.AddDays(-1).Date)
                .FirstOrDefault();

            if (game is null)
                throw new ArgumentException("This team has not more games.");

            Forecast forecast = await this.ForecastService.GetForecastAsync(game.Stadium);

            return new GameWeather(game, forecast);
        }
    }
}
