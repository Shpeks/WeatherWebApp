using Microsoft.AspNetCore.Mvc;
using System.Net.WebSockets;
using WeatherWebApp.Models;

namespace WeatherWebApp.Controllers
{
    public class WeatherController : Controller
    {
        private readonly WeatherService _weatherService;

        public WeatherController(WeatherService weatherService)
        {
            _weatherService = weatherService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetWeather(string city)
        {
            var weatherData = await _weatherService.GetWeatherDataAsync(city);
            
            double temperatureInCelsius = weatherData.Main.Temp - 273.15;
            var weatherViewModel = new MainWeather
            {
                City = city,
                Temp = Math.Round(temperatureInCelsius, 1)
            };

            return PartialView("/Views/Weather/_WeatherPartial.cshtml", weatherViewModel);
        }
    }
}
