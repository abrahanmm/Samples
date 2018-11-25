using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace NFLGameWeather.Model.Services
{
    public class ForecastService : IForecastService
    {
        public async Task<Forecast> GetForecastAsync(float geoLatitude, float geoLongitude, DateTime date)
        {
            using (HttpClient client = new HttpClient())
            {
                string apiKey = "MSTYhksuum4CfZgJFaqyHXPukFGrr4Ya";
                string path = $"http://dataservice.accuweather.com/locations/v1/cities/geoposition/search?apikey={apiKey}&q={geoLatitude.ToString(CultureInfo.InvariantCulture.NumberFormat)},{geoLongitude.ToString(CultureInfo.InvariantCulture.NumberFormat)}";
                HttpResponseMessage responseLocation = await client.GetAsync(path);
                dynamic location = JObject.Parse(await responseLocation.Content.ReadAsStringAsync());
                path = $"http://dataservice.accuweather.com/forecasts/v1/daily/5day/{location.Key}?apikey={apiKey}&metric=true";
                HttpResponseMessage responseForecast = await client.GetAsync(path);
                dynamic dynamicForecast = JObject.Parse(await responseForecast.Content.ReadAsStringAsync());

                return ((IEnumerable)dynamicForecast.DailyForecasts).Cast<dynamic>()
                    .Where(x => ((DateTime)x.Date).Date == date.Date)
                    .Select(x => new Forecast { Minimum = x.Temperature.Minimum.Value + "° C", Maximum = x.Temperature.Maximum.Value + "° C", Day = x.Day.IconPhrase, Night = x.Night.IconPhrase })
                    .FirstOrDefault();
            }
        }
    }
}
