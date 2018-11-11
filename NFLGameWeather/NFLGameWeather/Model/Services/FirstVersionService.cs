using NFLGameWeather.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Globalization;
using Newtonsoft.Json.Linq;
using System.Collections;

namespace NFLGameWeather.Model.Services
{
    public class FirstVersionService
    {
        public async Task<GameWeather> GetGameWeatherAsync(string teamKey)
        {
            if (string.IsNullOrWhiteSpace(teamKey))
                throw new ArgumentNullException("The key of the team can not be null.");

            Team team = Team.GetTeams().Where(x => x.Key.Equals(teamKey.ToUpper())).FirstOrDefault();

            if (team is null)
                throw new ArgumentException("The key is not from any team.");

            Game game = Game.Schedule
                .Where(x => x.HomeTeam.Equals(team) || x.AwayTeam.Equals(team))
                .Where(x => x.Date.Date > DateTime.UtcNow.AddDays(-1).Date)
                .FirstOrDefault();

            if (game is null)
                throw new ArgumentException("This team has not more games.");

            Forecast forecast;

            using (HttpClient client = new HttpClient())
            {
                string apiKey = "MSTYhksuum4CfZgJFaqyHXPukFGrr4Ya";
                string path = $"http://dataservice.accuweather.com/locations/v1/cities/geoposition/search?apikey={apiKey}&q={game.Stadium.GeoLatitude.ToString(CultureInfo.InvariantCulture.NumberFormat)},{game.Stadium.GeoLongitude.ToString(CultureInfo.InvariantCulture.NumberFormat)}";
                HttpResponseMessage responseLocation = await client.GetAsync(path);
                dynamic location = JObject.Parse(await responseLocation.Content.ReadAsStringAsync());
                path = $"http://dataservice.accuweather.com/forecasts/v1/daily/5day/{location.Key}?apikey={apiKey}&metric=true";
                HttpResponseMessage responseForecast = await client.GetAsync(path);
                dynamic dynamicForecast = JObject.Parse(await responseForecast.Content.ReadAsStringAsync());

                forecast = ((IEnumerable)dynamicForecast.DailyForecasts).Cast<dynamic>()
                    .Where(x => ((DateTime)x.Date).Date == DateTime.Now.Date)
                    .Select(x => new Forecast { Minimum = x.Temperature.Minimum.Value + "° C", Maximum = x.Temperature.Maximum.Value + "° C", Day = x.Day.IconPhrase, Night = x.Night.IconPhrase })
                    .FirstOrDefault();
            }

            return new GameWeather(game, forecast);
        }
    }
}
