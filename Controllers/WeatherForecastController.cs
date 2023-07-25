using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace SignalR.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IHubContext<ProgressHub> _hubContext;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IHubContext<ProgressHub> hubContext)
        {
            _logger = logger;
            _hubContext = hubContext;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpPost("startProcess")]
        public async Task<IActionResult> StartProcess()
        {
            // Some logic to start the long-running process
            for (int i = 0; i < 10; i++)
            {
                await Task.Delay(1000);

                // Report progress to the client
                await _hubContext.Clients.All.SendAsync("ReceiveProgressUpdate", $"Step {i + 1} completed.");
            }

            return Ok("[]");
        }
    }
}