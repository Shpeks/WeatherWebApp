using Newtonsoft.Json;
using WeatherWebApp.Models;

namespace WeatherWebApp
{
    public class WeatherService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public WeatherService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<WeatherData> GetWeatherDataAsync(string city)
        {
            var baseUrl = _configuration.GetValue<string>("OpenWeatherMapApiOptions:BaseUrl");
            var appId = _configuration.GetValue<string>("OpenWeatherMapApiOptions:AppId");

            var requestUrl = $"{baseUrl}?q={city}&appid={appId}";

            var response = await _httpClient.GetAsync(requestUrl); // Выполняем GET-запрос

            var responseContent = await response.Content.ReadAsStringAsync(); // Читаем содержимое ответа

            Console.WriteLine(responseContent); // Выводим содержимое в консоль

            var weatherData = JsonConvert.DeserializeObject<WeatherData>(responseContent); // Десериализуем данные

            return weatherData;
        }

    }

}
