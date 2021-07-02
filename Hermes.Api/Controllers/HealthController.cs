using Microsoft.AspNetCore.Mvc;

namespace Hermes.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Looks good!");
        }
    }
}
