using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NFLGameWeather.Model.Services
{
    public interface IForecastService
    {
        Task<Forecast> GetForecastAsync(Stadium stadium);
    }
}
