using System;
using System.Threading.Tasks;

namespace NFLGameWeather.Model.Services
{
    public interface IForecastService
    {
        /// <summary>
        /// Search a forecast for a geographical position on a date.
        /// </summary>
        /// <param name="geoLatitude">Geographical latitude of the forecast's position.</param>
        /// <param name="geoLongitude">Geographical longitude of the forecast's position.</param>
        /// <param name="date">Forecast's date.</param>
        /// <returns>Forecast for the position on the date.</returns>
        Task<Forecast> GetForecastAsync(float geoLatitude, float geoLongitude, DateTime date);
    }
}
