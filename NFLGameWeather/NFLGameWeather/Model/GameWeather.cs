using System;

namespace NFLGameWeather.Model
{
    public class GameWeather
    {
        public GameWeather(Game game, Forecast forecast)
        {
            this.AwayTeam = game.AwayTeam.FullName;
            this.HomeTeam = game.HomeTeam.FullName;
            this.City = game.Stadium.City;
            this.Stadium = game.Stadium.Name;
            this.Date = game.Date;
            this.Forecast = forecast;
        }

        public string AwayTeam { get; }

        public string HomeTeam { get; }

        public string Stadium { get; }

        public string City { get; }

        public DateTime Date { get; }

        public Forecast Forecast { get; }
    }
}
