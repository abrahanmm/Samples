using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace NFLGameWeather.Model.Services
{
    public class GameWeatherService : IGameWeatherService
    {
        private IForecastService ForecastService { get; }
        private ILogger<IGameWeatherService> Logger { get; }

        public GameWeatherService(IForecastService forecastService, ILogger<IGameWeatherService> logger)
        {
            this.ForecastService = forecastService;
            this.Logger = logger;
        }

        public async Task<GameWeather> GetNextGameWeatherAsync(string teamKey)
        {
            if (string.IsNullOrWhiteSpace(teamKey))
            {
                this.Logger.LogWarning("Empty key");
                throw new ArgumentNullException(nameof(teamKey));
            }

            Team team = Team.GetTeams().Where(x => x.Key.Equals(teamKey.ToUpper())).FirstOrDefault();

            if (team is null)
            {
                this.Logger.LogWarning($"The key {teamKey} is not from any team.");
                throw new ArgumentException("The key is not from any team.");
            }

            Game game = Game.Schedule
                .Where(x => x.HomeTeam.Equals(team) || x.AwayTeam.Equals(team))
                .Where(x => x.Date.Date > DateTime.UtcNow.AddDays(-1).Date)
                .FirstOrDefault();

            if (game is null)
            {
                this.Logger.LogWarning($"Not found more games for {team.FullName}.");
                throw new ArgumentException("This team has not more games.");
            }

            Forecast forecast = await this.ForecastService.GetForecastAsync(game.Stadium);

            this.Logger.LogInformation($"Found forecast for the game {game.AwayTeam.FullName} vs {game.HomeTeam.FullName}.");

            return new GameWeather(game, forecast);
        }
    }
}
