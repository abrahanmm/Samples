using Microsoft.AspNetCore.Mvc;
using NFLGameWeather.Model;
using NFLGameWeather.Model.Services;
using System.Threading.Tasks;

namespace NFLGameWeather.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FirstVersionController : Controller
    {
        [HttpGet("{teamKey}")]
        public async Task<ActionResult<GameWeather>> Get(string teamKey)
        {
            var service = new FirstVersionService();
            return await service.GetNextGameWeatherAsync(teamKey);
        }
    }
}