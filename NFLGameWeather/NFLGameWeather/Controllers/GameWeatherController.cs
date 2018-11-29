using Microsoft.AspNetCore.Mvc;
using NFLGameWeather.Model;
using NFLGameWeather.Model.Services;
using System.Threading.Tasks;

namespace NFLGameWeather.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameWeatherController : Controller
    {
        private IGameWeatherService GameWeatherService { get; }

        public GameWeatherController(IGameWeatherService gameWeatherService)
        {
            this.GameWeatherService = gameWeatherService;
        }

        [HttpGet("{teamKey}")]
        public async Task<ActionResult<GameWeather>> Get(string teamKey)
        {
            return await this.GameWeatherService.GetNextGameWeatherAsync(teamKey);
        }
    }
}